using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IRatingRepository
    {
        Task<int> CreateAsync(Rating ratingToCreate);
    }
}
