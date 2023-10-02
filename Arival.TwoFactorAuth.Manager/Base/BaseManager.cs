using Arival.TwoFactorAuth.Entities;
using Arival.TwoFactorAuth.Entities.CustomException;

namespace Arival.TwoFactorAuth.Manager {
    public class BaseManager {
        protected void ValidateRequestPayload<TRequestEntity>(TRequestEntity requestEntity, string errorCode, string errorMessage) {
            if(requestEntity == null) {
                throw new ApiValidationsException(ErrorType.InvalidInput, errorCode, errorMessage);
            }
        }
    }
}
