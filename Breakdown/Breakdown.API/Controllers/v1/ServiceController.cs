using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels;
using Breakdown.API.ViewModels.Service;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IMapper autoMapper, IServiceRepository serviceRepository)
        {
            _autoMapper = autoMapper;
            _serviceRepository = serviceRepository;
        }

        [Authorize]
        [HttpGet("api/v1/Service")]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Service> allServices = await _serviceRepository.RetrieveAsync(null);
                if (allServices.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                IEnumerable<ServiceGetViewModel> allServiceViewModels = _autoMapper.Map<IEnumerable<ServiceGetViewModel>>(allServices);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Services = allServiceViewModels });
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
        [HttpGet("api/v1/Service/{serviceId:int}")]
        public async Task<ActionResult> GetById(int serviceId)
        {
            if (serviceId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                IEnumerable<Service> requestedService = await _serviceRepository.RetrieveAsync(serviceId);
                if (requestedService.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                ServiceGetViewModel requestedServiceViewModel = _autoMapper.Map<ServiceGetViewModel>(requestedService.SingleOrDefault());
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Service = requestedServiceViewModel });
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
        [HttpPost("api/v1/Service")]
        public async Task<ActionResult> Create(ServiceBaseViewModel model)
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

                Service serviceEntityToCreate = _autoMapper.Map<Service>(model);
                int affectedRows = await _serviceRepository.CreateAsync(serviceEntityToCreate);
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
        [HttpPut("api/v1/Service")]
        public async Task<ActionResult> Update(ServiceUpdateViewModel model)
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

                Service serviceEntityToUpdate = _autoMapper.Map<Service>(model);
                int affectedRows = await _serviceRepository.UpdateAsync(serviceEntityToUpdate);
                if (affectedRows <= 0)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.UpdateFailed
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
        [HttpDelete("api/v1/Service/{serviceId:int}")]
        public async Task<ActionResult> Delete(int serviceId)
        {
            if (serviceId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                int affectedRows = await _serviceRepository.DeleteAsync(serviceId);
                if (affectedRows <= 0)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.DeleteFailed
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
    }
}
