namespace Arival.TwoFactorAuth.Interfaces.Repository {
    public interface IDatabaseContext {
        IAuthCodeRepository AuthCode { get; set; }
    }
}
