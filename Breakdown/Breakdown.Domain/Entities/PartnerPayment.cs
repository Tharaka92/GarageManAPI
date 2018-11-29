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

        public int? TotalCashJobs { get; set; }

        public int? TotalCardJobs { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalCashAmount { get; set; }

        public decimal TotalCardAmount { get; set; }

        public decimal CashAppFee { get; set; }

        public decimal CardAppFee { get; set; }

        public decimal TotalCardEarning { get; set; }

        public decimal TotalCashEarning { get; set; }

        public bool HasPaid { get; set; }

        public bool HasReceived { get; set; }

        public ApplicationUser Partner { get; set; }
    }
}
