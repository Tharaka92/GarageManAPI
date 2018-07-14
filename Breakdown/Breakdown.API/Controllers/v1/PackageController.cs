using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.Contracts.DTOs;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
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

        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Package> allPackages = await _packageRepository.Retrieve(null);
                if (allPackages.Count() > 0)
                {
                    IEnumerable<PackageDto> allPackagesDtos = _autoMapper.Map<IEnumerable<PackageDto>>(allPackages);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Packages = allPackagesDtos });
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

        public async Task<ActionResult> GetById(int packageId)
        {
            try
            {
                IEnumerable<Package> requestedPackage = await _packageRepository.Retrieve(packageId);
                if (requestedPackage.Count() > 0)
                {
                    PackageDto requestedPackageDto = _autoMapper.Map<PackageDto>(requestedPackage.SingleOrDefault());
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Package = requestedPackageDto });
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
        public async Task<ActionResult> Create([FromBody]PackageDto packageToCreate)
        {
            try
            {
                if (packageToCreate != null)
                {
                    Package packageEntityToCreate = _autoMapper.Map<Package>(packageToCreate);
                    int affectedRows = await _packageRepository.Create(packageEntityToCreate);
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
        public async Task<ActionResult> Update([FromBody]PackageDto packageToUpdate)
        {
            try
            {
                if (packageToUpdate != null)
                {
                    Package packageEntityToUpdate = _autoMapper.Map<Package>(packageToUpdate);
                    int affectedRows = await _packageRepository.Update(packageEntityToUpdate);
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
        public async Task<ActionResult> Delete(int packageId)
        {
            try
            {
                int affectedRows = await _packageRepository.Delete(packageId);
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