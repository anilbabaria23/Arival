using Arival.TwoFactorAuth.Common.Configuration;
using Arival.TwoFactorAuth.Common.Helpers;
using Arival.TwoFactorAuth.Entities;
using Arival.TwoFactorAuth.Entities.CustomException;
using Arival.TwoFactorAuth.Entities.RequestEntities;
using Arival.TwoFactorAuth.Interfaces.Manager;
using Arival.TwoFactorAuth.Interfaces.Repository;
using Arival.TwoFactorAuth.Manager.Helper;
using Arival.TwoFactorAuth.Repository.Data.Entities;
using Microsoft.Extensions.Logging;

namespace Arival.TwoFactorAuth.Manager {
    public class AuthCodeManager : BaseManager, IAuthCodeManager {
        private readonly GlobalConfiguration globalConfiguration;
        private readonly IDatabaseContext databaseContext;
        private readonly IMemoryHelper memoryHelper;
        private readonly ILogger<AuthCodeManager> logger = null;
        public AuthCodeManager(GlobalConfiguration globalConfiguration,
            IDatabaseContext databaseContext,
            IMemoryHelper memoryHelper,
            ILogger<AuthCodeManager> logger) {
            this.globalConfiguration = globalConfiguration;
            this.databaseContext = databaseContext;
            this.memoryHelper = memoryHelper;
            this.logger = logger;
        }

        #region Public Methods
        public async Task<string> Generate2FACode(Generate2FARequestEntity requestEntity) {
            try {
                ValidateRequestPayload(requestEntity, ErrorCode.Invalid2FACodeRequest, ErrorMessage.Invalid2FACodeRequest);
                if(string.IsNullOrEmpty(requestEntity.MobileNumber)) {
                    throw new ApiValidationsException(ErrorType.InvalidInput, ErrorCode.PhoneNumberMissing, ErrorMessage.PhoneNumberMissing);
                }
                await ValidateConcurrentActiveCode(requestEntity.MobileNumber);

                string authCode = AuthCodeGenerator.GenerateRandomAuthCode(globalConfiguration.AuthCodeConfig.AuthCodeRandomCharRange, globalConfiguration.AuthCodeConfig.AuthCodeSize);
                string hashedCode = HashHelper.CreateHash($"{authCode}{DateTime.UtcNow:yyyyMMddHHmm}", globalConfiguration.AuthCodeConfig.AuthCodeHashLength, globalConfiguration.AuthCodeConfig.HashIteration);

                if(!string.IsNullOrEmpty(authCode)) {
                    var twoFactorAuthCode = new TwoFactorAuthentication() {
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTime.UtcNow,
                        IsVerified = false,
                        MobileNumber = requestEntity.MobileNumber,
                        VerificationCode = hashedCode
                    };

                    await this.databaseContext.AuthCode.Save(twoFactorAuthCode);
                    return authCode;
                }
            } finally {
                this.memoryHelper.ReleaseMemory();
            }

            return string.Empty;
        }

        public async Task<bool> Verify2FACode(Verify2FACodeRequestEntity requestEntity) {
            ValidateRequestPayload(requestEntity, ErrorCode.Invalid2FACodeVerificationRequest, ErrorMessage.Invalid2FACodeVerificationRequest);
            if(string.IsNullOrEmpty(requestEntity.MobileNumber)) {
                throw new ApiValidationsException(ErrorType.InvalidInput, ErrorCode.PhoneNumberMissing, ErrorMessage.PhoneNumberMissing);
            }
            if(string.IsNullOrEmpty(requestEntity.VerificationCode)) {
                throw new ApiValidationsException(ErrorType.InvalidInput, ErrorCode.AuthCodeMissing, ErrorMessage.AuthCodeMissing);
            }

            try {
                List<TwoFactorAuthentication> lstTwoFactorAuthCodes = await this.databaseContext.AuthCode.GetTwoFactorAuthenticationCodes(requestEntity.MobileNumber);
                if(lstTwoFactorAuthCodes?.Any() == false) {
                    return false;
                }

                foreach(var authCode in lstTwoFactorAuthCodes) {
                    int begin = 0;
                    bool isSuccess;
                    do {
                        isSuccess = HashHelper.VerifyHash($"{requestEntity.VerificationCode}{DateTime.UtcNow.AddMinutes(-begin).ToString("yyyyMMddHHmm")}", authCode.VerificationCode, globalConfiguration.AuthCodeConfig.AuthCodeHashLength);
                        begin++;
                    } while(isSuccess == false && begin <= globalConfiguration.AuthCodeConfig.CodeTTLInMinutes);

                    if(isSuccess) {
                        await this.databaseContext.AuthCode.UpdateCodeVerificationStatus(authCode.Id);
                        return true;
                    }
                }
            } finally {
                this.memoryHelper.ReleaseMemory();
            }

            return false;
        }

        #endregion

        #region Private Methods
        private async Task ValidateConcurrentActiveCode(string mobileNumber) {
            int totalConcurrentCode = await this.databaseContext.AuthCode.GetConcurrentActiveCodeCount(mobileNumber);
            if(totalConcurrentCode >= globalConfiguration.AuthCodeConfig.ConcurrentCodeThreshold) {
                throw new ApiValidationsException(ErrorType.InvalidInput, ErrorCode.MaxConcurrentCodeLimit, string.Format(ErrorMessage.MaxConcurrentCodeLimit, mobileNumber));
            }
        }
        #endregion
    }
}
