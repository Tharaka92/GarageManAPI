﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Breakdown.API.Constants;
using Breakdown.API.ViewModels.VehicleTypes;
using Breakdown.Contracts.Interfaces;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleTypeController(IMapper autoMapper, IVehicleTypeRepository vehicleTypeRepository)
        {
            _autoMapper = autoMapper;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        [HttpGet("api/v1/VehicleType")]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<VehicleType> vehicleTypes = await _vehicleTypeRepository.RetrieveAsync(null);
                if (vehicleTypes.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                IEnumerable<VehicleTypeGetViewModel> vehicleTypeGetVms = _autoMapper.Map<IEnumerable<VehicleTypeGetViewModel>>(vehicleTypes);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Response = vehicleTypeGetVms });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Response = ResponseConstants.InternalServerError });
            }
        }

        [HttpGet("api/v1/VehicleType/{vehicleTypeId:int}")]
        public async Task<ActionResult> Get(int vehicleTypeId)
        {
            try
            {
                IEnumerable<VehicleType> vehicleType = await _vehicleTypeRepository.RetrieveAsync(vehicleTypeId);
                if (vehicleType.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                VehicleTypeGetViewModel vehicleTypeGetVm = _autoMapper.Map<VehicleTypeGetViewModel>(vehicleType.SingleOrDefault());
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Response = vehicleTypeGetVm });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Response = ResponseConstants.InternalServerError });
            }
        }

        [HttpPost("api/v1/VehicleType")]
        public async Task<ActionResult> Create(VehicleTypePostViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            try
            {
                if (!TryValidateModel(model))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.ValidationFailure });
                }

                VehicleType vehicleTypeEntity = _autoMapper.Map<VehicleType>(model);
                await _vehicleTypeRepository.CreateAsync(vehicleTypeEntity);

                return StatusCode(StatusCodes.Status201Created, new { IsSucceeded = true });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Response = ResponseConstants.InternalServerError });
            }
        }

        [HttpPut("api/v1/VehicleType")]
        public async Task<ActionResult> Update(VehicleTypeUpdateViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            try
            {
                if (!TryValidateModel(model))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.ValidationFailure });
                }

                VehicleType vehicleTypeEntity = _autoMapper.Map<VehicleType>(model);
                await _vehicleTypeRepository.UpdateAsync(vehicleTypeEntity);

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Response = ResponseConstants.InternalServerError });
            }
        }

        [HttpDelete("api/v1/VehicleType/{vehicleTypeId:int}")]
        public async Task<ActionResult> Delete(int vehicleTypeId)
        {
            if (vehicleTypeId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });

            }

            try
            {
                await _vehicleTypeRepository.DeleteAsync(vehicleTypeId);
                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false, Message = "Internal Server Error Occured." });
            }
        }
    }
}