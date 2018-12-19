using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Payment
{
    public class PaymentDetailsResponseViewModel
    {
        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string AccountHolderName { get; set; }

        public string AccountNumber { get; set; }
    }
}
