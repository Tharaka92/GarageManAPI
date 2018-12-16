using Breakdown.Domain.DTOs;
using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Interfaces
{
    public interface IPartnerPaymentRepository
    {
        Task<int> CreateAsync(PartnerPayment recordToCreate);
        Task<PartnerPayment> RetrieveAsync(int PartnerId);
    }
}
