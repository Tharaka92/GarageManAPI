using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.DTOs
{
    public class BraintreeSettingsDto
    {
        public string BraintreeEnvironment { get; set; }
        public string BraintreeMerchantId { get; set; }
        public string BraintreePublicKey { get; set; }
        public string BraintreePrivateKey { get; set; }
        public string BraintreeMerchantAccountId { get; set; }
    }
}
