using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.Options
{
    public class PaymentDetailsOptions
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
    }
}
