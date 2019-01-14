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
using Microsoft.AspNetCore.Identity;
using Breakdown.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBraintreeConfiguration _braintreeConfig;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IPartnerPaymentRepository _partnerPaymentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<PaymentDetailsOptions> _paymentDetails;

        public PaymentController(IBraintreeConfiguration braintreeConfig,
                                 IServiceRequestRepository serviceRequestRepository,
                                 IPartnerPaymentRepository partnerPaymentRepository,
                                 UserManager<ApplicationUser> userManager,
                                 IOptions<PaymentDetailsOptions> paymentDetails)
        {
            _braintreeConfig = braintreeConfig;
            _serviceRequestRepository = serviceRequestRepository;
            _partnerPaymentRepository = partnerPaymentRepository;
            _userManager = userManager;
            _paymentDetails = paymentDetails;
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

                if (model.IsCard)
                {
                    var transactionResult = await _braintreeConfig.CreateSale(model.Nonce, model.TotalAmount);
                    if (!transactionResult.IsSuccess())
                    {
                        return StatusCode(StatusCodes.Status417ExpectationFailed, new { IsSucceeded = false, Response = ResponseConstants.BraintreeCheckoutFailed });
                    }

                }

                await _serviceRequestRepository.UpdatePaymentDetailsAsync(model.ServiceRequestId,
                                                                          model.TotalAmount,
                                                                          model.PackagePrice,
                                                                          model.PartnerAmount,
                                                                          model.PaymentStatus,
                                                                          model.IsCard ? PaymentTypes.CARD : PaymentTypes.CASH);

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, model.IsCard });

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
        [HttpPost("api/v1/Payment/CalculatePartnerEarnings")]
        public async Task<IActionResult> CalculatePartnerEarnings(PartnerEarningsRequestViewModel model)
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

                if (model.PartnerId <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
                }

                var partnerPaymentDto = await _serviceRequestRepository.RetrievePaymentAmountAsync(model.PartnerId, model.FromDate, model.ToDate);
                if (partnerPaymentDto == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                if (partnerPaymentDto.CashCount == 0) //Only accept Cash payments from frontend apps for now.
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var partnerPaymentToCreate = Mapper.Map<PartnerPayment>(partnerPaymentDto);
                //partnerPaymentToCreate.AppFee += partnerPaymentToCreate.CashCount * 100;

                if (model.IsNewPaymentCycle)
                {
                    partnerPaymentToCreate.PartnerId = model.PartnerId;
                    partnerPaymentToCreate.AppFeeRemainingAmount = partnerPaymentToCreate.AppFee;
                    partnerPaymentToCreate.FromDate = model.FromDate;
                    partnerPaymentToCreate.ToDate = model.ToDate;
                    partnerPaymentToCreate.CreatedDate = DateTime.Now; //Get Sri lankan Time

                    int affectedRows = await _partnerPaymentRepository.CreateAsync(partnerPaymentToCreate);
                    if (affectedRows > 0)
                    {
                        var appUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == model.PartnerId);
                        appUser.HasADuePayment = true;
                        await _userManager.UpdateAsync(appUser);
                    }
                }

                var partnerEarningsVm = Mapper.Map<PartnerEarningsResponseViewModel>(partnerPaymentToCreate);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, PartnerEarnings = partnerEarningsVm });
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
        [HttpGet("api/v1/Payment/GetOverDuePartnerFee")]
        public async Task<IActionResult> GetOverDuePartnerFee(int partnerId)
        {
            if (partnerId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                var partnerPayment = await _partnerPaymentRepository.RetrieveAsync(partnerId);
                if (partnerPayment == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var overDuePartnerFeeVm = Mapper.Map<OverDuePartnerFeeViewModel>(partnerPayment);

                var paymentDetailsVm = new PaymentDetailsResponseViewModel
                {
                    BankName = _paymentDetails.Value.BankName,
                    BranchName = _paymentDetails.Value.BranchName,
                    AccountHolderName = _paymentDetails.Value.AccountHolderName,
                    AccountNumber = _paymentDetails.Value.AccountNumber,
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    IsSucceeded = true,
                    OverDuePartnerFee = overDuePartnerFeeVm,
                    PaymentDetails = paymentDetailsVm
                });
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