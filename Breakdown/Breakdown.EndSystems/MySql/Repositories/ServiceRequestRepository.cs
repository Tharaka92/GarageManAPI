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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breakdown.Domain.DTOs;

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public ServiceRequestRepository(IOptions<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ServiceRequest serviceRequestToCreate)
        {
            try
            {
                SPInsertServiceRequest parameters = new SPInsertServiceRequest
                {
                    CustomerId = serviceRequestToCreate.CustomerId,
                    PartnerId = serviceRequestToCreate.PartnerId,
                    ServiceId = serviceRequestToCreate.ServiceId,
                    VehicleTypeId = serviceRequestToCreate.VehicleTypeId,
                    PackageId = serviceRequestToCreate.PackageId,
                    SubmittedDate = serviceRequestToCreate.SubmittedDate,
                    ServiceRequestStatus = serviceRequestToCreate.ServiceRequestStatus,
                    PaymentStatus = serviceRequestToCreate.PaymentStatus
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

        public async Task<int> GetLatestServiceRequestIdAsync(int partnerId, int customerId, string serviceRequestStatus)
        {
            try
            {
                SPRetrieveLatestServiceRequestId parameters = new SPRetrieveLatestServiceRequestId
                {
                    CustomerId = customerId,
                    PartnerId = partnerId,
                    ServiceRequestStatus = serviceRequestStatus
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    var ids = await connection.QueryAsync<int>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                    return ids.ToList().SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateServiceRequestStatusAsync(int serviceRequestId, string serviceRequestStatus)
        {
            try
            {
                SPUpdateServiceRequestStatus parameters = new SPUpdateServiceRequestStatus
                {
                    ServiceRequestId = serviceRequestId,
                    ServiceRequestStatus = serviceRequestStatus
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

        public async Task<int> CompleteServiceRequestAsync(int serviceRequestId,
                                                           string serviceRequestStatus,
                                                           DateTime startDate,
                                                           DateTime endDate)
        {
            try
            {
                SPCompleteServiceRequest parameters = new SPCompleteServiceRequest
                {
                    ServiceRequestId = serviceRequestId,
                    ServiceRequestStatus = serviceRequestStatus,
                    StartDate = startDate,
                    EndDate = endDate
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

        public async Task<int> UpdatePaymentDetailsAsync(int serviceRequestId,
                                                   decimal totalAmount,
                                                   decimal packagePrice,
                                                   decimal partnerAmount,
                                                   string paymentStatus,
                                                   string paymentType)
        {
            try
            {
                SPUpdatePaymentDetails parameters = new SPUpdatePaymentDetails
                {
                    ServiceRequestId = serviceRequestId,
                    TotalAmount = totalAmount,
                    PackagePrice = packagePrice,
                    PartnerAmount = partnerAmount,
                    PaymentStatus = paymentStatus,
                    PaymentType = paymentType
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

        public async Task<PartnerPaymentDto> RetrievePaymentAmountAsync(int partnerId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                SPRetrievePaymentAmount parameters = new SPRetrievePaymentAmount
                {
                    PartnerId = partnerId,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                DynamicParameters dynamicParameters = new DynamicParameters(parameters);
                dynamicParameters.Add("AppFee", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                dynamicParameters.Add("TotalCashAmount", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                dynamicParameters.Add("TotalCardAmount", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                dynamicParameters.Add("CashCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add("CardCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql: parameters.GetName(), param: dynamicParameters, commandType: CommandType.StoredProcedure);

                    PartnerPaymentDto returnDto = new PartnerPaymentDto
                    {
                        AppFee = dynamicParameters.Get<decimal>("AppFee"),
                        TotalCardAmount = dynamicParameters.Get<decimal>("TotalCardAmount"),
                        TotalCashAmount = dynamicParameters.Get<decimal>("TotalCashAmount"),
                        CardCount = dynamicParameters.Get<int>("CardCount"),
                        CashCount = dynamicParameters.Get<int>("CashCount")
                    };

                    return returnDto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
