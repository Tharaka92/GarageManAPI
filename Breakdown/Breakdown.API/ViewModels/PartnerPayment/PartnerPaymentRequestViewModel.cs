using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.PartnerPayment
{
    public class PartnerPaymentRequestViewModel
    {
        [Required]
        public int PartnerId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        public bool IsNewPaymentCycle { get; set; }
    }
}
