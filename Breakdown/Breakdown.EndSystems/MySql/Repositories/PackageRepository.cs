using Breakdown.Contracts.Interfaces;
using Breakdown.Contracts.Options;
using Breakdown.Domain.Entities;
using Breakdown.EndSystems.MySql.StoredProcedures;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public PackageRepository(IOptions<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Package packageToCreate)
        {
            try
            {
                SPInsertPackage parameters = new SPInsertPackage()
                {
                    ServiceId = packageToCreate.ServiceId,
                    VehicleTypeId = packageToCreate.VehicleTypeId,
                    Name = packageToCreate.Name,
                    Description = packageToCreate.Description,
                    Price = packageToCreate.Price
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(int packageId)
        {
            try
            {
                SPDeletePackage parameters = new SPDeletePackage()
                {
                    PackageId = packageId
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Package>> RetrieveAsync(int? packageId, int? serviceId, int? vehicleTypeId)
        {
            try
            {
                SPRetrievePackage parameters = new SPRetrievePackage
                {
                    PackageId = packageId.HasValue ? packageId : null,
                    ServiceId = serviceId.HasValue ? serviceId : null,
                    VehicleTypeId = vehicleTypeId.HasValue ? vehicleTypeId : null
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.QueryAsync<Package>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(Package packageToUpdate)
        {
            try
            {
                SPUpdatePackage parameters = new SPUpdatePackage()
                {
                    PackageId = packageToUpdate.PackageId,
                    Name = packageToUpdate.Name,
                    Description = packageToUpdate.Description,
                    Price = packageToUpdate.Price
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
