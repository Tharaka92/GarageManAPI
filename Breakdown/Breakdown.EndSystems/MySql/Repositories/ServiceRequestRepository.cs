﻿using Breakdown.Contracts.DTOs;
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
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public ServiceRequestRepository(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ServiceRequest serviceRequestToCreate)
        {
            try
            {
                SPInsertServiceRequest parameters = new SPInsertServiceRequest()
                {
                    CustomerId = serviceRequestToCreate.CustomerId,
                    PartnerId = serviceRequestToCreate.PartnerId,
                    ServiceId = serviceRequestToCreate.ServiceId,
                    VehicleTypeId = serviceRequestToCreate.VehicleTypeId,
                    PackageId = serviceRequestToCreate.PackageId,
                    StartDate = serviceRequestToCreate.StartDate,
                    ServiceRequestStatus = serviceRequestToCreate.ServiceRequestStatus,
                    PaymentStatus = serviceRequestToCreate.PaymentStatus,
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
