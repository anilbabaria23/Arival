using Newtonsoft.Json;

namespace Arival.TwoFactorAuth.Entities.ResponseEntities {
    public class Generate2FAResponseEntity {
        [JsonProperty("auth_code_generated")]
        public bool IsAuthCodeGenerated { get; set; }

        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }
    }
}
