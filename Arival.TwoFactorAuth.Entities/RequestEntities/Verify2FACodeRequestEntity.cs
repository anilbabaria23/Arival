namespace Arival.TwoFactorAuth.Entities.RequestEntities {
    public class Verify2FACodeRequestEntity {
        public string MobileNumber { get; set; }

        public string VerificationCode { get; set; }
    }
}
