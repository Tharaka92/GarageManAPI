using Breakdown.Contracts.DTOs;
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
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public VehicleRepository(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(Vehicle vehicleToCreate)
        {
            try
            {
                SPInsertVehicle parameters = new SPInsertVehicle()
                {
                    LicensePlate = vehicleToCreate.LicensePlate,
                    VehicleType = vehicleToCreate.VehicleType,
                    Make = vehicleToCreate.Make,
                    Model = vehicleToCreate.Model,
                    Color = vehicleToCreate.Color,
                    YOM = vehicleToCreate.YOM
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

        public async Task<int> Delete(int vehicleId)
        {
            try
            {
                SPDeleteVehicle parameters = new SPDeleteVehicle()
                {
                    VehicleId = vehicleId
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

        public async Task<IEnumerable<Vehicle>> Retrieve(int? vehicleId)
        {
            try
            {
                SPRetrieveVehicle parameters = new SPRetrieveVehicle
                {
                    VehicleId = vehicleId.HasValue ? vehicleId : null
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.QueryAsync<Vehicle>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Update(Vehicle vehicleToUpdate)
        {
            try
            {
                SPUpdateVehicle parameters = new SPUpdateVehicle()
                {
                    VehicleId = vehicleToUpdate.VehicleId,
                    LicensePlate = vehicleToUpdate.LicensePlate,
                    VehicleType = vehicleToUpdate.VehicleType,
                    Make = vehicleToUpdate.Make,
                    Model = vehicleToUpdate.Model,
                    Color = vehicleToUpdate.Color,
                    YOM = vehicleToUpdate.YOM
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
