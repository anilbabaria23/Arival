using Arival.TwoFactorAuth.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arival.TwoFactorAuth.Repository {
    public class BaseRepository {
        internal readonly ArivalDbContext arivalDbContext;
        public BaseRepository(ArivalDbContext dbContext) {
            this.arivalDbContext = dbContext;
        }

        internal DbContext DbContext {
            get {
                return this.arivalDbContext;
            }
        }
    }
}
