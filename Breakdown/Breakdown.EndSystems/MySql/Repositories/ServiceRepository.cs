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
    public class ServiceRepository : IServiceRepository
    {
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public ServiceRepository(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(Service serviceToCreate)
        {
            try
            {
                SPInsertService parameters = new SPInsertService()
                {
                    ServiceName = serviceToCreate.ServiceName
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

        public async Task<int> Delete(int serviceId)
        {
            try
            {
                SPDeleteService parameters = new SPDeleteService()
                {
                    ServiceId = serviceId
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

        public async Task<IEnumerable<Service>> Retrieve(int? serviceId)
        {
            try
            {
                SPRetrieveService parameters = new SPRetrieveService
                {
                    ServiceId = serviceId.HasValue ? serviceId : null
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    return await connection.QueryAsync<Service>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Update(Service serviceToUpdate)
        {
            try
            {
                SPUpdateService parameters = new SPUpdateService()
                {
                    ServiceId = serviceToUpdate.ServiceId,
                    ServiceName = serviceToUpdate.ServiceName
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
