using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> Create(Service serviceToCreate);
        Task<IEnumerable<Service>> Retrieve(int? serviceId);
        Task<int> Update(Service serviceToUpdate);
        Task<int> Delete(int serviceId);
    }
}
