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
    public class RatingRepository : IRatingRepository
    {
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public RatingRepository(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Rating ratingToCreate)
        {
            try
            {
                SPInsertRating parameters = new SPInsertRating()
                {
                   UserId = ratingToCreate.UserId,
                   ServiceRequestId = ratingToCreate.ServiceRequestId,
                   RatingValue = ratingToCreate.RatingValue,
                   Comment = ratingToCreate.Comment
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
