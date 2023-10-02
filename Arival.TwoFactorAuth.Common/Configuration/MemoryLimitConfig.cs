namespace Arival.TwoFactorAuth.Common.Configuration {
    public class MemoryLimitConfig {
        
        public const string MemoryLimitConfigKey = "MemoryLimitConfig";

        public bool Enabled { get; set; }

        public long Interval { get; set; }

        public long ThreshHold { get; set; }
    }
}
