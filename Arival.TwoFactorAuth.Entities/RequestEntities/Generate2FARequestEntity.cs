using Newtonsoft.Json;

namespace Arival.TwoFactorAuth.Entities.RequestEntities {
    public class Generate2FARequestEntity {
        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }
    }
}
