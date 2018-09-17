using Braintree;
using Breakdown.Contracts.Braintree;
using Breakdown.Contracts.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.EndSystems.Braintree
{
    public class BraintreeConfiguration : IBraintreeConfiguration
    {
        private readonly IOptions<BraintreeSettingsDto> _braintreeSettings;
        private BraintreeGateway BraintreeGateway;
        private string Environment { get; set; }
        private string MerchantId { get; set; }
        private string PublicKey { get; set; }
        private string PrivateKey { get; set; }

        public BraintreeConfiguration(IOptions<BraintreeSettingsDto> braintreeSettings)
        {
            _braintreeSettings = braintreeSettings;

            Environment = _braintreeSettings.Value.BraintreeEnvironment;
            MerchantId = _braintreeSettings.Value.BraintreeMerchantId;
            PublicKey = _braintreeSettings.Value.BraintreePublicKey;
            PrivateKey = _braintreeSettings.Value.BraintreePrivateKey;

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
