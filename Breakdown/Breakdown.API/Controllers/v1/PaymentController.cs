﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Breakdown.Contracts.Braintree;
using Breakdown.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IBraintreeConfiguration _braintreeConfig;

        public PaymentController(IBraintreeConfiguration braintreeConfig)
        {
            _braintreeConfig = braintreeConfig;
        }

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

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] PaymentDto paymentModel)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _braintreeConfig.CreateSale(paymentModel.Nonce, paymentModel.Amount));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}