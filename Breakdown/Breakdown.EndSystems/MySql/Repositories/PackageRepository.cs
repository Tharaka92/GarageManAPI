using Breakdown.Contracts.DataAccess;
using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        public int Create(Package packageToCreate)
        {
            throw new NotImplementedException();
        }

        public int Delete(int packageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> Retrieve(int? packageId)
        {
            throw new NotImplementedException();
        }

        public int Update(Package packageToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
