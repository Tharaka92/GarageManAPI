using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Breakdown.EndSystems.IdentityConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class ExpiredTokenRepository : IExpiredTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExpiredTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(ExpiredToken tokenToExpire)
        {
            await _dbContext.Set<ExpiredToken>().AddAsync(tokenToExpire);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
