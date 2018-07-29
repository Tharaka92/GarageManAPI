using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class PaymentViewModel
    {
        public string Nonce { get; set; }
        public decimal Amount { get; set; }
    }
}
