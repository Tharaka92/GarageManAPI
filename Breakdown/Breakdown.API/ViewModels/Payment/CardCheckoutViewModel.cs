using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Payment
{
    public class CardCheckoutViewModel
    {
        [Required]
        public int ServiceRequestId { get; set; }

        [Required]
        public string Nonce { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal PackagePrice { get; set; }

        public decimal TipAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public string PaymentType { get; set; }
    }
}
