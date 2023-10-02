namespace Arival.TwoFactorAuth.Common.Helpers {
    public interface IMemoryHelper {
        void ClearGCMemory();
        void ReleaseMemory();
    }
}
