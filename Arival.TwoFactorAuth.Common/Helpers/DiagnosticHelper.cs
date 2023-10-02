using System.Diagnostics;

namespace Arival.TwoFactorAuth.Common.Helpers {
    public class DiagnosticHelper {
        private static Process _process = Process.GetCurrentProcess();
        private static readonly int _mb = 1024 * 1024;

        public static long GetUsedMemory() {
            _process.Refresh();
            return _process.WorkingSet64 / _mb;
        }
    }
}
