using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleMicroservice.API.Models;
using VehicleMicroservice.Contracts.Models;
using VehicleMicroservice.Core.Services;
using VehicleMicroservice.Models;

namespace VehicleMicroservice.Controllers
{
    /// <summary>
    /// Operations to manage vehicles 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class VehiclesController : Controller
    {

        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(IVehicleService vehicleService,  ILogger<VehiclesController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        /// <summary>
        /// Returns the complete set of all vehicles.
        /// </summary>
        /// <remarks>
        /// <para>Returns the complete set of all vehicles 
        /// </para>
        /// </remarks>
        /// <returns>The set of filtered vehicles</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Vehicle>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAll();
            return Ok(vehicles);
        }

        /// <summary>
        /// Returns the requested vehicles.
        /// </summary>
        /// <remarks>
        /// <para>Returns the vehicles specified by ID.
        /// </para>
        /// </remarks>
        /// <returns>The set of requested vehicles</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleById(Guid id)
        {
            var vehicle = await _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }


        /// <summary>
        /// Create a new vehicle.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new vehicle, allocating an ID in the process.
        /// </para>
        /// </remarks>
        /// <returns>Returns newly created vehicle, including its ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            var newVehicle = await _vehicleService.Create(vehicle);
            return Ok(newVehicle);
        }

        /// <summary>
        /// Updates an new vehicle.
        /// </summary>
        /// <remarks>
        /// <para>Updates the mutable attributes of the supplied vehicle.
        /// </para>
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVehicle(Guid id, Vehicle vehicle)
        {
            var existingVehicle = await _vehicleService.GetById(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }
            await _vehicleService.Update(vehicle);
            return Ok();
        }

        /// <summary>
        /// Deletes a vehicle.
        /// </summary>
        /// <remarks>
        /// <para>Deletes the specified vehicle.
        /// </para>
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var existingVehicle = await _vehicleService.GetById(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }
            await _vehicleService.DeleteById(id);
            return Ok();
        }
    }
}
