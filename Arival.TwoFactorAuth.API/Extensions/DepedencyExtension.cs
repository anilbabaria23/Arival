using Arival.TwoFactorAuth.Common.Configuration;
using Arival.TwoFactorAuth.Common.Helpers;
using Arival.TwoFactorAuth.Interfaces.Manager;
using Arival.TwoFactorAuth.Interfaces.Repository;
using Arival.TwoFactorAuth.Manager;
using Arival.TwoFactorAuth.Repository;
using Arival.TwoFactorAuth.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Arival.TwoFactorAuth.API.Extensions {
    public static class DepedencyExtension {
        public static IServiceCollection RegisterDependency(this IServiceCollection services, GlobalConfiguration globalConfiguration) {

            services.AddSingleton(globalConfiguration);
            RegisterDatabaseDependency(services, globalConfiguration);
            services.AddScoped<IAuthCodeManager, AuthCodeManager>();
            services.AddSingleton<IMemoryHelper, MemoryHelper>();
            return services;
        }

        private static void RegisterDatabaseDependency(IServiceCollection services, GlobalConfiguration globalConfiguration) {
            if(!string.IsNullOrEmpty(globalConfiguration.DatabaseConnectionString)) {
                if(globalConfiguration.CommandTimeOut > 0) {
                    services.AddDbContextPool<ArivalDbContext>(options => options.UseSqlServer(globalConfiguration.DatabaseConnectionString, (serverOptions) => {
                        serverOptions.CommandTimeout(globalConfiguration.CommandTimeOut);
                        serverOptions.EnableRetryOnFailure(globalConfiguration.CommandTimeOutRetry);
                    }), globalConfiguration.DBPoolSize);
                } else {
                    services.AddDbContextPool<ArivalDbContext>(options => options.UseSqlServer(globalConfiguration.DatabaseConnectionString, serverOptions => serverOptions.EnableRetryOnFailure(globalConfiguration.CommandTimeOutRetry)), globalConfiguration.DBPoolSize);
                }
            }

            services.AddScoped<IDatabaseContext, DatabaseContext>();
        }
    }
}
