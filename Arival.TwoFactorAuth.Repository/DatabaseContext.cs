using Arival.TwoFactorAuth.Interfaces.Repository;
using Arival.TwoFactorAuth.Repository.Data;

namespace Arival.TwoFactorAuth.Repository {
    public class DatabaseContext : IDatabaseContext {
        public DatabaseContext() {

        }

        public DatabaseContext(ArivalDbContext dbContext) {
            this.AuthCode = new AuthCodeRepository(dbContext);
        }

        public IAuthCodeRepository AuthCode { get; set; }
    }
}
