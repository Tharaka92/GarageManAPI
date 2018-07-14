using Breakdown.Contracts.Interfaces;
using Breakdown.Contracts.DTOs;
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
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public PackageRepository(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(Package packageToCreate)
        {
            try
            {
                SPInsertPackage parameters = new SPInsertPackage()
                {
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

        public async Task<int> Delete(int packageId)
        {
            try
            {
                SPDeletePackage parameters = new SPDeletePackage()
                {
                    PackageId = packageId,
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

        public async Task<IEnumerable<Package>> Retrieve(int? packageId)
        {
            try
            {
                SPRetrievePackage parameters = new SPRetrievePackage
                {
                    PackageId = packageId.HasValue ? packageId : null
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

        public async Task<int> Update(Package packageToUpdate)
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
