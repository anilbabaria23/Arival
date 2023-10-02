namespace Arival.TwoFactorAuth.Common.Configuration {
    public class GlobalConfiguration {
        public const string CommandTimeoutKeyConfig = "DBCommandTimeOut";
        public const string CommandTimeoutRetryKeyConfig = "DBCommandTimeOutRetry";
        public const string DBPoolSizeKeyConfig = "DBPoolSize";
        public GlobalConfiguration() {
            this.AuthCodeConfig = new AuthCodeConfig();
            this.MemoryLimitConfig= new MemoryLimitConfig();
        }
        
        public AuthCodeConfig AuthCodeConfig { get; set; }

        public MemoryLimitConfig MemoryLimitConfig { get; set; }

        public string DatabaseConnectionString { get; set; }

        public int CommandTimeOut { get; set; }

        public int CommandTimeOutRetry { get; set; }

        public int DBPoolSize { get; set; }
    }
}