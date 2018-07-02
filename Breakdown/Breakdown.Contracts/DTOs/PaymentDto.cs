using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.DTOs
{
    public class PaymentDto
    {
        public string Nonce { get; set; }
        public decimal Amount { get; set; }
    }
}
