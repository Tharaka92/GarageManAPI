using Braintree;
using Breakdown.Contracts.Braintree;
using Breakdown.Contracts.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.Braintree
{
    public class BraintreeConfiguration : IBraintreeConfiguration
    {
        private readonly IOptions<BraintreeOptions> _braintreeOptions;
        private BraintreeGateway BraintreeGateway;
        private string Environment { get; set; }
        private string MerchantId { get; set; }
        private string PublicKey { get; set; }
        private string PrivateKey { get; set; }
        public string BraintreeMerchantAccountId { get; set; }

        public BraintreeConfiguration(IOptions<BraintreeOptions> braintreeOptions)
        {
            _braintreeOptions = braintreeOptions;

            Environment = _braintreeOptions.Value.BraintreeEnvironment;
            MerchantId = _braintreeOptions.Value.BraintreeMerchantId;
            PublicKey = _braintreeOptions.Value.BraintreePublicKey;
            PrivateKey = _braintreeOptions.Value.BraintreePrivateKey;
            BraintreeMerchantAccountId = _braintreeOptions.Value.BraintreeMerchantAccountId;

            BraintreeGateway = new BraintreeGateway(Environment, MerchantId, PublicKey, PrivateKey);
        }

        public async Task<string> GenerateToken()
        {
            return await BraintreeGateway.ClientToken.GenerateAsync();
        }

        public async Task<Result<Transaction>> CreateSale(string nonceFromTheClient, decimal price)
        {
            if (price > 0)
            {
                var request = new TransactionRequest
                {
                    Amount = price,
                    MerchantAccountId = BraintreeMerchantAccountId,
                    PaymentMethodNonce = nonceFromTheClient,
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }
                };

                return await BraintreeGateway.Transaction.SaleAsync(request);
            }
            else
            {
                return null;
            }
        }
    }
}
