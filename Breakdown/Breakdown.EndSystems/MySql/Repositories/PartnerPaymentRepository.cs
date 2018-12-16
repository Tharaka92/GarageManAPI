using Breakdown.Contracts.Interfaces;
using Breakdown.Contracts.Options;
using Breakdown.Domain.DTOs;
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

namespace Breakdown.EndSystems.MySql.Repositories
{
    public class PartnerPaymentRepository : IPartnerPaymentRepository
    {
        private readonly IOptions<ConnectionStringOptions> _connectionString;

        public PartnerPaymentRepository(IOptions<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(PartnerPayment recordToCreate)
        {
            try
            {
                SPInsertPartnerPayment parameters = new SPInsertPartnerPayment
                {
                    PartnerId = recordToCreate.PartnerId,
                    CashCount = recordToCreate.CashCount,
                    CardCount = recordToCreate.CardCount,
                    FromDate = recordToCreate.FromDate,
                    ToDate = recordToCreate.ToDate,
                    CreatedDate = recordToCreate.CreatedDate,
                    TotalCashAmount = recordToCreate.TotalCashAmount,
                    TotalCardAmount = recordToCreate.TotalCardAmount,
                    AppFee = recordToCreate.AppFee,
                    AppFeePaidAmount = recordToCreate.AppFeePaidAmount,
                    AppFeeRemainingAmount = recordToCreate.AppFeeRemainingAmount,
                    HasPaid = recordToCreate.HasPaid,
                    HasReceived = recordToCreate.HasReceived
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

        public async Task<PartnerPayment> RetrieveAsync(int partnerId)
        {
            try
            {
                SPRetrievePartnerPayment parameters = new SPRetrievePartnerPayment
                {
                    PartnerId = partnerId
                };

                using (DbConnection connection = DbConnectionFactory.GetConnection(_connectionString.Value.BreakdownDb))
                {
                    connection.Open();
                    var partnerPayments = await connection.QueryAsync<PartnerPayment>(sql: parameters.GetName(), param: parameters, commandType: CommandType.StoredProcedure);
                    return partnerPayments.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
