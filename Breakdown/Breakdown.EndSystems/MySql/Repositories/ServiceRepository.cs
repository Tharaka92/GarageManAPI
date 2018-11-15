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
    public class ServiceRepository : IServiceRepository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public ServiceRepository(IOptions<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Service serviceToCreate)
        {
            try
            {
                SPInsertService parameters = new SPInsertService()
                {
                    ServiceName = serviceToCreate.ServiceName,
                    UniqueCode = serviceToCreate.UniqueCode
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

        public async Task<int> DeleteAsync(int serviceId)
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

        public async Task<IEnumerable<Service>> RetrieveAsync(int? serviceId)
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

        public async Task<int> UpdateAsync(Service serviceToUpdate)
        {
            try
            {
                SPUpdateService parameters = new SPUpdateService()
                {
                    ServiceId = serviceToUpdate.ServiceId,
                    ServiceName = serviceToUpdate.ServiceName,
                    UniqueCode = serviceToUpdate.UniqueCode
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
