using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.DTOs
{
    public class BraintreeSettingsDto
    {
        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
