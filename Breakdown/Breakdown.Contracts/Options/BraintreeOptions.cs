using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.Options
{
    public class BraintreeOptions
    {
        public string BraintreeEnvironment { get; set; }
        public string BraintreeMerchantId { get; set; }
        public string BraintreePublicKey { get; set; }
        public string BraintreePrivateKey { get; set; }
        public string BraintreeMerchantAccountId { get; set; }
    }
}
