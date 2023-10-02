using Newtonsoft.Json;

namespace Arival.TwoFactorAuth.Entities.ResponseEntities {
    public class Verify2FAResponseEntity {
        [JsonProperty("is_valid_code")]
        public bool IsValidCode { get; set; }

    }
}
