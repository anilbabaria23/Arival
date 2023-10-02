using System.ComponentModel.DataAnnotations;

namespace Arival.TwoFactorAuth.Repository.Data.Entities {
    public class TwoFactorAuthentication {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(15)]
        public string MobileNumber { get; set; }

        [MaxLength(200)]
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
