using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IPackageRepository
    {
        Task<int> Create(Package packageToCreate);
        Task<IEnumerable<Package>> Retrieve(int? packageId, int? serviceId);
        Task<int> Update(Package packageToUpdate);
        Task<int> Delete(int packageId);
    }
}
