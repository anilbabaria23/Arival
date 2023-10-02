using Arival.TwoFactorAuth.Common.Configuration;
using Microsoft.Extensions.Logging;

namespace Arival.TwoFactorAuth.Common.Helpers {
    public class MemoryHelper : IMemoryHelper {
        private readonly ILogger<MemoryHelper> logger = null;
        private readonly GlobalConfiguration globalConfiguration = null;

        public MemoryHelper(GlobalConfiguration globalConfiguration, ILoggerFactory loggerFactory) {
            this.globalConfiguration = globalConfiguration;
            this.logger = loggerFactory.CreateLogger<MemoryHelper>();
        }
        public void ClearGCMemory() {
            var useMemory = DiagnosticHelper.GetUsedMemory();
            if(useMemory > globalConfiguration?.MemoryLimitConfig.ThreshHold) {
                logger.LogInformation($"Detected used memory {useMemory}MB, which is higher then threshold configured {globalConfiguration?.MemoryLimitConfig.ThreshHold}MB. Clearing memory.");
                this.ReleaseMemory();
            }
        }

        public void ReleaseMemory() {
            GC.Collect(1, GCCollectionMode.Forced, false);
            GC.Collect(2, GCCollectionMode.Forced, false);
        }
    }
}
