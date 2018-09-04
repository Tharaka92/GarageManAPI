using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.ViewModels;
using Breakdown.Contracts.DTOs;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class PackageController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IPackageRepository _packageRepository;

        public PackageController(IMapper autoMapper, IPackageRepository packageRepository)
        {
            _autoMapper = autoMapper;
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Package> allPackages = await _packageRepository.RetrieveAsync(null, null);
                if (allPackages.Count() > 0)
                {
                    IEnumerable<PackageViewModel> allPackageViewModels = _autoMapper.Map<IEnumerable<PackageViewModel>>(allPackages);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = allPackageViewModels });
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int packageId)
        {
            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.RetrieveAsync(packageId, null);
                if (requestedPackage.Count() > 0)
                {
                    PackageViewModel requestedPackageViewModel = _autoMapper.Map<PackageViewModel>(requestedPackage.SingleOrDefault());
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Package = requestedPackageViewModel });
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetByServiceId(int serviceId)
        {
            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.RetrieveAsync(null, serviceId);
                if (requestedPackage.Count() > 0)
                {
                    IEnumerable<PackageViewModel> requestedPackageViewModels = _autoMapper.Map<IEnumerable<PackageViewModel>>(requestedPackage);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = requestedPackageViewModels });
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(PackageViewModel packageToCreate)
        {
            try
            {
                if (packageToCreate != null)
                {
                    Package packageEntityToCreate = _autoMapper.Map<Package>(packageToCreate);
                    int affectedRows = await _packageRepository.CreateAsync(packageEntityToCreate);
                    if (affectedRows == 1)
                    {
                        return StatusCode(StatusCodes.Status201Created, new { IsSucceeded = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status417ExpectationFailed, new { IsSucceeded = false });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(PackageViewModel packageToUpdate)
        {
            try
            {
                if (packageToUpdate != null)
                {
                    Package packageEntityToUpdate = _autoMapper.Map<Package>(packageToUpdate);
                    int affectedRows = await _packageRepository.UpdateAsync(packageEntityToUpdate);
                    if (affectedRows == 1)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status417ExpectationFailed, new { IsSucceeded = false });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int packageId)
        {
            try
            {
                int affectedRows = await _packageRepository.DeleteAsync(packageId);
                if (affectedRows == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
                }
                else
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new { IsSucceeded = false });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }
    }
}