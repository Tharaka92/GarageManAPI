using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IServiceRequestRepository
    {
        Task<int> CreateAsync(ServiceRequest serviceRequestToCreate);
        Task<int> GetLatestServiceRequestIdAsync(int partnerId, int customerId, string serviceRequestStatus);
        Task<int> UpdateServiceRequestStatusAsync(int serviceRequestId, string serviceRequestStatus);
        Task<int> CompleteServiceRequestAsync(int serviceRequestId, string serviceRequestStatus, DateTime startDate, DateTime endDate);
    }
}
