using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IExpiredTokenRepository
    {
        Task<int> Create(ExpiredToken tokenToExpire);
    }
}
