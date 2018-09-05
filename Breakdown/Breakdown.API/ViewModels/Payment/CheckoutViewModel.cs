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
        public string Nonce { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
