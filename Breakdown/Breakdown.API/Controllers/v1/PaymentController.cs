using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels.Payment;
using Breakdown.Contracts.Braintree;
using Breakdown.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBraintreeConfiguration _braintreeConfig;

        public PaymentController(IBraintreeConfiguration braintreeConfig)
        {
            _braintreeConfig = braintreeConfig;
        }

        [HttpGet("api/v1/Payment/GetToken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _braintreeConfig.GenerateToken());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("api/v1/Payment/Checkout")]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            try
            {
                if (!TryValidateModel(model))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.ValidationFailure
                    });
                }

                return StatusCode(StatusCodes.Status200OK, await _braintreeConfig.CreateSale(model.Nonce, model.Amount));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}