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
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IMapper autoMapper, IVehicleRepository vehicleRepository)
        {
            _autoMapper = autoMapper;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Vehicle> allVehicles = await _vehicleRepository.Retrieve(null);
                if (allVehicles.Count() > 0)
                {
                    IEnumerable<VehicleDto> allVehicleDtos = _autoMapper.Map<IEnumerable<VehicleDto>>(allVehicles);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Vehicles = allVehicleDtos });
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

        public async Task<ActionResult> GetById(int vehicleId)
        {
            try
            {
                IEnumerable<Vehicle> requestedVehicle = await _vehicleRepository.Retrieve(vehicleId);
                if (requestedVehicle.Count() > 0)
                {
                    VehicleDto requestedVehicleDto = _autoMapper.Map<VehicleDto>(requestedVehicle.SingleOrDefault());
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Vehicle = requestedVehicleDto });
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
        public async Task<ActionResult> Create([FromBody]VehicleDto vehicleToCreate)
        {
            try
            {
                if (vehicleToCreate != null)
                {
                    Vehicle vehicleEntityToCreate = _autoMapper.Map<Vehicle>(vehicleToCreate);
                    int affectedRows = await _vehicleRepository.Create(vehicleEntityToCreate);
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
        public async Task<ActionResult> Update([FromBody]VehicleDto vehicleToUpdate)
        {
            try
            {
                if (vehicleToUpdate != null)
                {
                    Vehicle vehicleEntityToUpdate = _autoMapper.Map<Vehicle>(vehicleToUpdate);
                    int affectedRows = await _vehicleRepository.Update(vehicleEntityToUpdate);
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
        public async Task<ActionResult> Delete(int vehicleId)
        {
            try
            {
                int affectedRows = await _vehicleRepository.Delete(vehicleId);
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