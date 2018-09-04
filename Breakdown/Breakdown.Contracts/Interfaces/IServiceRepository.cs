using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> CreateAsync(Service serviceToCreate);
        Task<IEnumerable<Service>> RetrieveAsync(int? serviceId);
        Task<int> UpdateAsync(Service serviceToUpdate);
        Task<int> DeleteAsync(int serviceId);
    }
}
