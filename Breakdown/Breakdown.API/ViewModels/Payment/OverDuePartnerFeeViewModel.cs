using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Payment
{
    public class OverDuePartnerFeeViewModel
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        //public int CardCount { get; set; }

        public int CashCount { get; set; }

        //public decimal TotalCardAmount { get; set; }

        public decimal TotalCashAmount { get; set; }

        public decimal AppFee { get; set; }

        public decimal TotalCashEarnings => TotalCashAmount - AppFee;
    }
}
