using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Domain.DTOs
{
    public class PartnerPaymentDto
    {
        public decimal AppFee { get; set; }

        public decimal TotalCashAmount { get; set; }

        public decimal TotalCardAmount { get; set; }

        public int CashCount { get; set; }

        public int CardCount { get; set; }
    }
}
