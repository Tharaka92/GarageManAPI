﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels.ServiceRequest;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestController(IMapper autoMapper, IServiceRequestRepository serviceRequestRepository)
        {
            _autoMapper = autoMapper;
            _serviceRequestRepository = serviceRequestRepository;
        }

        [Authorize]
        [HttpGet("api/v1/ServiceRequest/GetLatestServiceRequestId")]
        public async Task<ActionResult> GetLatestServiceRequestId(int partnerId, int customerId, string serviceRequestStatus)
        {
            if (partnerId <= 0 || customerId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            if (string.IsNullOrEmpty(serviceRequestStatus))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                int latestServiceRequestId = await _serviceRequestRepository.GetLatestServiceRequestIdAsync(partnerId, customerId, serviceRequestStatus);
                if (latestServiceRequestId <= 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, LatestServiceRequestId = latestServiceRequestId });
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
        [HttpPost("api/v1/ServiceRequest")]
        public async Task<ActionResult> Create(ServiceRequestPostViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            if (model.CustomerId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            if (model.PartnerId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            if (model.ServiceId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            if (model.PackageId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
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

                ServiceRequest serviceRequestEntity = _autoMapper.Map<ServiceRequest>(model);
                int affectedRows = await _serviceRequestRepository.CreateAsync(serviceRequestEntity);
                if (affectedRows <= 0)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.CreateFailed
                    });
                }

                return StatusCode(StatusCodes.Status201Created, new { IsSucceeded = true });
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
        [HttpGet("api/v1/ServiceRequest")]
        public async Task<ActionResult> Get(int? partnerId, int? customerId, int skip = 0, int take = 20)
        {
            if (partnerId <= 0 || customerId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                var serviceRequestDtos = await _serviceRequestRepository.Retrieve(partnerId, customerId, null, skip, take);
                if (serviceRequestDtos.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var serviceRequestVms = Mapper.Map<IEnumerable<ServiceRequestGetViewModel>>(serviceRequestDtos);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, ServiceRequests = serviceRequestVms });
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
        [HttpPut("api/v1/ServiceRequest/UpdateServiceRequestStatus")]
        public async Task<ActionResult> UpdateServiceRequestStatus(UpdateServiceRequestStatusViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            if (model.ServiceRequestId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
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

                int affectedRows = await _serviceRequestRepository.UpdateServiceRequestStatusAsync(model.ServiceRequestId, model.ServiceRequestStatus);
                if (affectedRows <= 0)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.CancelFailed
                    });
                }

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

        [Authorize]
        [HttpPut("api/v1/ServiceRequest/CompleteServiceRequest")]
        public async Task<ActionResult> CompleteServiceRequest(CompleteServiceRequestViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            if (model.ServiceRequestId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
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

                int affectedRows = await _serviceRequestRepository.CompleteServiceRequestAsync(model.ServiceRequestId,
                                                                                               model.ServiceRequestStatus,
                                                                                               model.StartDate,
                                                                                               model.EndDate);
                if (affectedRows <= 0)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.CancelFailed
                    });
                }

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