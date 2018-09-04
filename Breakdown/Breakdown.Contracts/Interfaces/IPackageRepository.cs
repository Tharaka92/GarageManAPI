using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IPackageRepository
    {
        Task<int> CreateAsync(Package packageToCreate);
        Task<IEnumerable<Package>> RetrieveAsync(int? packageId, int? serviceId);
        Task<int> UpdateAsync(Package packageToUpdate);
        Task<int> DeleteAsync(int packageId);
    }
}
