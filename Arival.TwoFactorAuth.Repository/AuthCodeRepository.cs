using Arival.TwoFactorAuth.Interfaces.Repository;
using Arival.TwoFactorAuth.Repository.Data;
using Arival.TwoFactorAuth.Repository.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arival.TwoFactorAuth.Repository {
    public class AuthCodeRepository : BaseRepository, IAuthCodeRepository {
        public AuthCodeRepository(ArivalDbContext dbContext) : base(dbContext) {

        }

        public async Task Save(TwoFactorAuthentication twoFactorAuthentication) {
            await this.DbContext.Set<TwoFactorAuthentication>().AddAsync(twoFactorAuthentication);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<int> GetConcurrentActiveCodeCount(string mobileNumber) {
            return await this.DbContext.Set<TwoFactorAuthentication>().AsNoTracking().Where(x => x.MobileNumber == mobileNumber && x.IsVerified == false).CountAsync();
        }

        public async Task<List<TwoFactorAuthentication>> GetTwoFactorAuthenticationCodes(string mobileNumber) {
            return await this.DbContext.Set<TwoFactorAuthentication>().AsNoTracking().Where(x => x.MobileNumber == mobileNumber && x.IsVerified == false && x.CreatedOn.Date == DateTime.UtcNow.Date).ToListAsync();
        }

        public async Task UpdateCodeVerificationStatus(Guid id) {
            var twoFactorAuthCode = await this.DbContext.Set<TwoFactorAuthentication>().Where(x => x.Id == id).FirstOrDefaultAsync();
            twoFactorAuthCode.IsVerified = true;
            this.DbContext.Update(twoFactorAuthCode);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
