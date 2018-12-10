using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPInsertPartnerPayment : SP
    {
        public int PartnerId { get; set; }
        public int TotalCashJobs { get; set; }
        public int TotalCardJobs { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalCashAmount { get; set; }
        public decimal TotalCardAmount { get; set; }
        public decimal AppFee { get; set; }
        public decimal AppFeePaidAmount { get; set; }
        public decimal AppFeeRemainingAmount { get; set; }
        public bool HasPaid { get; set; }
        public bool HasReceived { get; set; }
    }
}
