using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IVehicleRepository
    {
        Task<int> Create(Vehicle vehicleToCreate);
        Task<IEnumerable<Vehicle>> Retrieve(int? vehicleId, string userId);
        Task<int> Update(Vehicle vehicleToUpdate);
        Task<int> Delete(int vehicleId);
    }
}
