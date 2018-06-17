using Braintree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.Contracts.Braintree
{
    public interface IBraintreeConfiguration
    {
        Task<string> GenerateToken();
        Task<Result<Transaction>> CreateSale(string nonceFromTheClient, decimal price);
    }
}
