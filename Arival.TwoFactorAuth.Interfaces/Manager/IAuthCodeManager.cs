using Arival.TwoFactorAuth.Entities.RequestEntities;

namespace Arival.TwoFactorAuth.Interfaces.Manager {
    public interface IAuthCodeManager {
        Task<string> Generate2FACode(Generate2FARequestEntity requestEntity);

        Task<bool> Verify2FACode(Verify2FACodeRequestEntity requestEntity);
    }
}
