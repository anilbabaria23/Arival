namespace Arival.TwoFactorAuth.Common.Configuration {
    public class AuthCodeConfig {

        public const string AuthenticationCodeConfigKey = "AuthenticationCodeConfig";

        public int ConcurrentCodeThreshold { get; set; }

        public int CodeTTLInMinutes { get; set; }

        public int HashIteration { get; set; }

        public int AuthCodeSize { get; set; }

        public int AuthCodeHashLength { get; set; }

        public string AuthCodeRandomCharRange { get; set; }
    }
}
