using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.ViewModels;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IMapper autoMapper, IServiceRepository serviceRepository)
        {
            _autoMapper = autoMapper;
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Service> allServices = await _serviceRepository.Retrieve(null);
                if (allServices.Count() > 0)
                {
                    IEnumerable<ServiceViewModel> allServiceViewModels = _autoMapper.Map<IEnumerable<ServiceViewModel>>(allServices);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Services = allServiceViewModels });
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error Occured." });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int serviceId)
        {
            try
            {
                IEnumerable<Service> requestedService = await _serviceRepository.Retrieve(serviceId);
                if (requestedService.Count() > 0)
                {
                    ServiceViewModel requestedServiceViewModel = _autoMapper.Map<ServiceViewModel>(requestedService.SingleOrDefault());
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Package = requestedServiceViewModel });
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error Occured." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ServiceViewModel serviceToCreate)
        {
            try
            {
                if (serviceToCreate != null)
                {
                    Service serviceEntityToCreate = _autoMapper.Map<Service>(serviceToCreate);
                    int affectedRows = await _serviceRepository.Create(serviceEntityToCreate);
                    if (affectedRows == 1)
                    {
                        return StatusCode(StatusCodes.Status201Created);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status417ExpectationFailed);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error Occured." });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(ServiceViewModel serviceToUpdate)
        {
            try
            {
                if (serviceToUpdate != null)
                {
                    Service serviceEntityToUpdate = _autoMapper.Map<Service>(serviceToUpdate);
                    int affectedRows = await _serviceRepository.Update(serviceEntityToUpdate);
                    if (affectedRows == 1)
                    {
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status417ExpectationFailed);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error Occured." });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int serviceId)
        {
            try
            {
                int affectedRows = await _serviceRepository.Delete(serviceId);
                if (affectedRows == 1)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error Occured." });
            }
        }
    }
}
