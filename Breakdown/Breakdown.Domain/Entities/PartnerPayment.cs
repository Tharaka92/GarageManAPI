using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class PartnerPayment
    {
        [Key]
        public int PartnerPaymentId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int PartnerId { get; set; }

        public int CashCount { get; set; }

        public int CardCount { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalCashAmount { get; set; }

        public decimal TotalCardAmount { get; set; }

        public decimal AppFee { get; set; }

        public decimal AppFeePaidAmount { get; set; }

        public decimal AppFeeRemainingAmount { get; set; }

        public bool HasPaid { get; set; }

        public bool HasReceived { get; set; }

        public ApplicationUser Partner { get; set; }
    }
}
