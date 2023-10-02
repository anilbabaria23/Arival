using Arival.TwoFactorAuth.Repository.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arival.TwoFactorAuth.Repository.Data {
    public class ArivalDbContext: DbContext {
        public ArivalDbContext(DbContextOptions<ArivalDbContext> options) : base(options) {

        }

        public DbSet<TwoFactorAuthentication> TwoFactorAuthentication { get; set; }


    }
}
