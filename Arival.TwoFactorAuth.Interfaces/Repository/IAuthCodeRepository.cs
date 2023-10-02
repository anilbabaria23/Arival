using Arival.TwoFactorAuth.Repository.Data.Entities;

namespace Arival.TwoFactorAuth.Interfaces.Repository {
    public interface IAuthCodeRepository {
        Task Save(TwoFactorAuthentication twoFactorAuthentication);

        Task<int> GetConcurrentActiveCodeCount(string mobileNumber);

        Task<List<TwoFactorAuthentication>> GetTwoFactorAuthenticationCodes(string mobileNumber);

        Task UpdateCodeVerificationStatus(Guid id);
    }
}
