using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Payment
{
    public class CheckoutViewModel
    {
        [Required]
        public int ServiceRequestId { get; set; }

        public string Nonce { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal PackagePrice { get; set; }

        [Required]
        public decimal PartnerAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public bool IsCard { get; set; }
    }
}
