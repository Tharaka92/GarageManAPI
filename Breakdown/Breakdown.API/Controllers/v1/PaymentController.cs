using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels.Payment;
using Breakdown.Contracts.Braintree;
using Breakdown.Contracts.Options;
using Breakdown.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBraintreeConfiguration _braintreeConfig;
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public PaymentController(IBraintreeConfiguration braintreeConfig, IServiceRequestRepository serviceRequestRepository)
        {
            _braintreeConfig = braintreeConfig;
            _serviceRequestRepository = serviceRequestRepository;
        }

        [Authorize]
        [HttpGet("api/v1/Payment/GetToken")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Token = await _braintreeConfig.GenerateToken() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    IsSucceeded = false,
                    Response = ResponseConstants.InternalServerError
                });
            }
        }

        [Authorize]
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

                var transactionResult = await _braintreeConfig.CreateSale(model.Nonce, model.TotalAmount);
                if (!transactionResult.IsSuccess())
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new { IsSucceeded = false, Response = ResponseConstants.BraintreeCheckoutFailed });
                }

                await _serviceRequestRepository.UpdatePaymentDetailsAsync(model.ServiceRequestId,
                                                                          model.TotalAmount,
                                                                          model.PackagePrice,
                                                                          model.TipAmount,
                                                                          model.PaymentStatus);

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    IsSucceeded = false,
                    Response = ResponseConstants.InternalServerError
                });
            }
        }
    }
}