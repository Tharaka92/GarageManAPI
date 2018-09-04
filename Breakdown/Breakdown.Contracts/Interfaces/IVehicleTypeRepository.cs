using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IVehicleTypeRepository
    {
        Task<int> CreateAsync(VehicleType vehicleTypeToCreate);
        Task<IEnumerable<VehicleType>> RetrieveAsync(int? vehicleTypeId);
        Task<int> UpdateAsync(VehicleType vehicleTypeToUpdate);
        Task<int> DeleteAsync(int vehicleTypeId);
    }
}
