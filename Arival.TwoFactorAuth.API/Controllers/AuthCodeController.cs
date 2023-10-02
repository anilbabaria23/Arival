using Arival.TwoFactorAuth.Common.Configuration;
using Arival.TwoFactorAuth.Entities.RequestEntities;
using Arival.TwoFactorAuth.Entities.ResponseEntities;
using Arival.TwoFactorAuth.Interfaces.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Arival.TwoFactorAuth.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCodeController : ControllerBase {
        private readonly IAuthCodeManager authCodeManager;
        private readonly ILogger<AuthCodeController> _logger;
        private readonly GlobalConfiguration globalConfiguration;

        public AuthCodeController(IAuthCodeManager authCodeManager, ILogger<AuthCodeController> logger, GlobalConfiguration globalConfiguration) {
            this.authCodeManager = authCodeManager;
            this._logger = logger;
            this.globalConfiguration = globalConfiguration;
        }

        [HttpPost("generate2FACode")]
        public async Task<Generate2FAResponseEntity> Generate([FromBody] Generate2FARequestEntity requestEntity) { 
            string generatedAuthCode = await authCodeManager.Generate2FACode(requestEntity);
            _logger.LogInformation($"Generated 2FA Code is: {generatedAuthCode} which will expire in next {globalConfiguration.AuthCodeConfig.CodeTTLInMinutes} minutes.");
            
            return new Generate2FAResponseEntity {
                IsAuthCodeGenerated = !string.IsNullOrEmpty(generatedAuthCode),
                AuthCode = generatedAuthCode
            };
        }

        [HttpPost("verify2FACode")]
        public async Task<Verify2FAResponseEntity> VerifyCode([FromBody] Verify2FACodeRequestEntity requestEntity) {
            bool isValidCode = await authCodeManager.Verify2FACode(requestEntity);

            return new Verify2FAResponseEntity {
                IsValidCode = isValidCode
            };
        }
    }
}
