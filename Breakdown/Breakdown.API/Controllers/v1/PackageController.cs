using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels;
using Breakdown.API.ViewModels.Package;
using Breakdown.Contracts.DTOs;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IPackageRepository _packageRepository;

        public PackageController(IMapper autoMapper, IPackageRepository packageRepository)
        {
            _autoMapper = autoMapper;
            _packageRepository = packageRepository;
        }

        [HttpGet("api/v1/Package")]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Package> allPackages = await _packageRepository.RetrieveAsync(null, null, null);
                if (allPackages.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                IEnumerable<PackageGetViewModel> allPackageViewModels = _autoMapper.Map<IEnumerable<PackageGetViewModel>>(allPackages);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = allPackageViewModels });
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

        [HttpGet("api/v1/Package/{packageId:int}")]
        public async Task<ActionResult> GetById(int packageId)
        {
            if (packageId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.RetrieveAsync(packageId, null, null);
                if (requestedPackage.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                PackageGetViewModel requestedPackageViewModel = _autoMapper.Map<PackageGetViewModel>(requestedPackage.SingleOrDefault());
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Package = requestedPackageViewModel });
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

        [HttpGet("api/v1/Package/GetByServiceId/{serviceId:int}")]
        public async Task<ActionResult> GetByServiceId(int serviceId)
        {
            if (serviceId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.RetrieveAsync(null, serviceId, null);
                if (requestedPackage.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                IEnumerable<PackageGetViewModel> requestedPackageViewModels = _autoMapper.Map<IEnumerable<PackageGetViewModel>>(requestedPackage);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = requestedPackageViewModels });

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

        [HttpGet("api/v1/Package/GetByVehicleTypeId/{vehicleTypeId:int}")]
        public async Task<ActionResult> GetByVehicleTypeId(int vehicleTypeId)
        {
            if (vehicleTypeId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.RetrieveAsync(null, null, vehicleTypeId);
                if (requestedPackage.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                IEnumerable<PackageGetViewModel> requestedPackageViewModels = _autoMapper.Map<IEnumerable<PackageGetViewModel>>(requestedPackage);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = requestedPackageViewModels });

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

        [HttpPost("api/v1/Package")]
        public async Task<ActionResult> Create(PackageBaseViewModel model)
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

                Package packageEntityToCreate = _autoMapper.Map<Package>(model);
                int affectedRows = await _packageRepository.CreateAsync(packageEntityToCreate);
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

        [HttpPut("api/v1/Package")]
        public async Task<ActionResult> Update(PackageUpdateViewModel model)
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

                Package packageEntityToUpdate = _autoMapper.Map<Package>(model);
                int affectedRows = await _packageRepository.UpdateAsync(packageEntityToUpdate);
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

        [HttpDelete("api/v1/Package/{packageId:int}")]
        public async Task<ActionResult> Delete(int packageId)
        {
            if (packageId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                int affectedRows = await _packageRepository.DeleteAsync(packageId);
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