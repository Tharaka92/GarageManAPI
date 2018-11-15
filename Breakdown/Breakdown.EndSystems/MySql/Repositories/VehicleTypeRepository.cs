using Breakdown.Contracts.Options;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Breakdown.EndSystems.MySql.StoredProcedures;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public VehicleTypeRepository(IOptions<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(VehicleType vehicleTypeToCreate)
        {
            try
            {
                SPInsertVehicleType parameters = new SPInsertVehicleType()
                {
                    Name = vehicleTypeToCreate.Name,
                    Description = vehicleTypeToCreate.Description,
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

        public async Task<int> DeleteAsync(int vehicleTypeId)
        {
            try
            {
                SPDeleteVehicleType parameters = new SPDeleteVehicleType()
                {
                    VehicleTypeId = vehicleTypeId
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

        public async Task<IEnumerable<VehicleType>> RetrieveAsync(int? vehicleTypeId)
        {
            try
            {
                SPRetrieveVehicleType parameters = new SPRetrieveVehicleType
                {
                    VehicleTypeId = vehicleTypeId.HasValue ? vehicleTypeId : null,
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.QueryAsync<VehicleType>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(VehicleType vehicleTypeToUpdate)
        {
            try
            {
                SPUpdateVehicleType parameters = new SPUpdateVehicleType()
                {
                    VehicleTypeId = vehicleTypeToUpdate.VehicleTypeId,
                    Name = vehicleTypeToUpdate.Name,
                    Description = vehicleTypeToUpdate.Description,
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
