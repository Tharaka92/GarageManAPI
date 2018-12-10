using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrievePaymentAmount : SP
    {
        public int PartnerId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public decimal AppFee { get; set; }

        public decimal TotalCashAmount { get; set; }

        public decimal TotalCardAmount { get; set; }

        public int CashCount { get; set; }

        public int CardCount { get; set; }
    }
}
