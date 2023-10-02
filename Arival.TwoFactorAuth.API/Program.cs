using Arival.TwoFactorAuth.API.Extensions;
using Arival.TwoFactorAuth.API.Middlewares;
using Arival.TwoFactorAuth.Common.Configuration;
using Arival.TwoFactorAuth.Common.Helpers;
using Arival.TwoFactorAuth.Repository.Data;
using Microsoft.EntityFrameworkCore;

public class Program {
    public static void Main(string[] args) {

        GlobalConfiguration globalConfiguration;

        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            globalConfiguration = GetConfig(builder);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterDependency(globalConfiguration);
        }

        var app = builder.Build();
        {
            if(app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using(var scope = app.Services.CreateScope()) {
                var dataContext = scope.ServiceProvider.GetRequiredService<ArivalDbContext>();
                dataContext.Database.Migrate();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            var memoryCleanerTimer = new System.Timers.Timer {
                Interval = globalConfiguration.MemoryLimitConfig.Interval * 1000,
                Enabled = globalConfiguration.MemoryLimitConfig.Enabled
            };
            memoryCleanerTimer.Elapsed += (sender, e) => ClearMemory(app);

            app.Run();
        }

    }
    private static void ClearMemory(WebApplication app) {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
        var memoryHelper = serviceScope.ServiceProvider.GetRequiredService<IMemoryHelper>();
        memoryHelper?.ClearGCMemory();
    }

    private static GlobalConfiguration GetConfig(WebApplicationBuilder builder) {
        var configBuilder = new ConfigurationBuilder()
         .SetBasePath(builder.Environment.ContentRootPath)
         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
         .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
         .AddEnvironmentVariables();

        IConfiguration config = configBuilder.Build();
        builder.Services.AddSingleton<IConfiguration>(config);
        GlobalConfiguration globalConfiguration = new() {
            DatabaseConnectionString = config.GetConnectionString("ArrivalConnStr"),
            CommandTimeOut = Convert.ToInt32(config[GlobalConfiguration.CommandTimeoutKeyConfig]),
            CommandTimeOutRetry = Convert.ToInt32(config[GlobalConfiguration.CommandTimeoutRetryKeyConfig]),
            DBPoolSize = Convert.ToInt32(config[GlobalConfiguration.DBPoolSizeKeyConfig])
        };
        config.GetSection(AuthCodeConfig.AuthenticationCodeConfigKey).Bind(globalConfiguration.AuthCodeConfig);
        config.GetSection(MemoryLimitConfig.MemoryLimitConfigKey).Bind(globalConfiguration.MemoryLimitConfig);
        return globalConfiguration;
    }
}
