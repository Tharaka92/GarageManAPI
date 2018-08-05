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
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IMapper autoMapper, IVehicleRepository vehicleRepository)
        {
            _autoMapper = autoMapper;
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                IEnumerable<Vehicle> allVehicles = await _vehicleRepository.Retrieve(null, null);
                if (allVehicles.Count() > 0)
                {
                    IEnumerable<VehicleViewModel> allVehicleViewModels = _autoMapper.Map<IEnumerable<VehicleViewModel>>(allVehicles);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Vehicles = allVehicleViewModels });
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
        public async Task<ActionResult> GetById(int vehicleId)
        {
            try
            {
                IEnumerable<Vehicle> requestedVehicle = await _vehicleRepository.Retrieve(vehicleId, null);
                if (requestedVehicle.Count() > 0)
                {
                    VehicleViewModel requestedVehicleViewModel = _autoMapper.Map<VehicleViewModel>(requestedVehicle.SingleOrDefault());
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Vehicle = requestedVehicleViewModel });
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
        public async Task<ActionResult> GetByUserId(int userId)
        {
            try
            {
                IEnumerable<Vehicle> requestedVehicle = await _vehicleRepository.Retrieve(null, userId);
                if (requestedVehicle.Count() > 0)
                {
                    IEnumerable<VehicleViewModel> requestedVehiclesViewModel = _autoMapper.Map<IEnumerable<VehicleViewModel>>(requestedVehicle);
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Vehicles = requestedVehiclesViewModel });
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
        public async Task<ActionResult> Create(VehicleViewModel vehicleToCreate)
        {
            try
            {
                if (vehicleToCreate != null)
                {
                    Vehicle vehicleEntityToCreate = _autoMapper.Map<Vehicle>(vehicleToCreate);
                    int affectedRows = await _vehicleRepository.Create(vehicleEntityToCreate);
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
        public async Task<ActionResult> Update(VehicleViewModel vehicleToUpdate)
        {
            try
            {
                if (vehicleToUpdate != null)
                {
                    Vehicle vehicleEntityToUpdate = _autoMapper.Map<Vehicle>(vehicleToUpdate);
                    int affectedRows = await _vehicleRepository.Update(vehicleEntityToUpdate);
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
        public async Task<ActionResult> Delete(int vehicleId)
        {
            try
            {
                int affectedRows = await _vehicleRepository.Delete(vehicleId);
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