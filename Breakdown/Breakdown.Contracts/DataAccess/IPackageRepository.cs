using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.DataAccess
{
    public interface IPackageRepository
    {
        int Create(Package packageToCreate);
        IEnumerable<Package> Retrieve(int? packageId);
        int Update(Package packageToUpdate);
        int Delete(int packageId);
    }
}
