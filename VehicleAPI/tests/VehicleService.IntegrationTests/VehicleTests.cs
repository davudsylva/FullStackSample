using Xunit;
using System.Linq;
using System.Collections.Generic;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VehicleMicroservice.Core.Services;
using VehicleMicroservice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using VehicleMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleMicroservice.Contracts.Models;
using System;
using VehicleMicroservice.API.Models;

namespace VehicleMicroservice.IntegrationTests
{
    // Integration tests design to test round trip to a live database. 
    public class VehicleTests : TestBase
    {
        [Fact]
        // Given a newly created vehicle
        // When I get that vehicle
        // Then I should be able to get the vehicle back
        public async Task CanCreateAndGetVehicle()
        {
            var controller = EstablishEnvironment();

            var testVehicle = new Vehicle 
            {
                VehicleType = "Car",
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = "Ford",
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = 4,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };

            // Ensure we can create a new vehicle
            var createResponse = await controller.CreateVehicle(testVehicle);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Vehicle;

            // Ensure we can get the product we just created
            var reGetResponse = await controller.GetVehicleById(createdItem.Id.Value);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var regotItem = reGetResult.Value as Vehicle;

            Assert.Equal(createdItem.Id, regotItem.Id);
            Assert.Equal(createdItem.VehicleType, regotItem.VehicleType);
            Assert.Equal(createdItem.VehicleCarDetails.Make, regotItem.VehicleCarDetails.Make);
            Assert.Equal(createdItem.VehicleCarDetails.Model, regotItem.VehicleCarDetails.Model);
            Assert.Equal(createdItem.VehicleCarDetails.Engine, regotItem.VehicleCarDetails.Engine);
            Assert.Equal(createdItem.VehicleCarDetails.Doors, regotItem.VehicleCarDetails.Doors);
            Assert.Equal(createdItem.VehicleCarDetails.Wheels, regotItem.VehicleCarDetails.Wheels);
            Assert.Equal(createdItem.VehicleCarDetails.BodyType, regotItem.VehicleCarDetails.BodyType);
        }


        [Fact]
        // Given a newly created vehicle
        // When I delete that vehcile
        // Then I should not be able to get it again
        public async Task CanCreateAndDeleteVehicle()
        {
            var controller = EstablishEnvironment();

            var testVehicle = new Vehicle
            {
                VehicleType = "Car",
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = "Ford",
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = 4,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };

            // Ensure we can create a new vehicle
            var createResponse = await controller.CreateVehicle(testVehicle);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Vehicle;

            // Ensure we can get the vehicle we just created
            var deleteResponse = await controller.DeleteVehicle(createdItem.Id.Value);
            var deleteResult = deleteResponse as OkResult;
            Assert.NotNull(deleteResult);

            // Ensure we can not get the product we just deleted
            var reGetResponse = await controller.GetVehicleById(createdItem.Id.Value);
            var reGetResult = reGetResponse as NotFoundResult;
            Assert.NotNull(reGetResult);
        }


        [Fact]
        // Given a newly created product
        // When I update that product
        // Then I should be able to get it again and see the changes
        public async Task CanCreateAndUpdateProduct()
        {
            var controller = EstablishEnvironment();

            var testVehicle = new Vehicle
            {
                VehicleType = "Car",
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = "Ford",
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = 4,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };

            // Ensure we can create a new product
            var createResponse = await controller.CreateVehicle(testVehicle);
            var okCreateResult = createResponse as OkObjectResult;
            Assert.NotNull(okCreateResult);
            var createdItem = okCreateResult.Value as Vehicle;

            createdItem.VehicleCarDetails.Model = "Focus";
            createdItem.VehicleCarDetails.BodyType = "Hatchback";
            createdItem.VehicleCarDetails.Doors = 5;

            // Ensure we can update the product we just created
            var updateResponse = await controller.UpdateVehicle(createdItem.Id.Value, createdItem);
            var updateResult = updateResponse as OkResult;
            Assert.NotNull(updateResult);

            // Ensure we can get the product we just updated
            var reGetResponse = await controller.GetVehicleById(createdItem.Id.Value);
            var reGetResult = reGetResponse as OkObjectResult;
            Assert.NotNull(reGetResult);
            var reGotItem = reGetResult.Value as Vehicle;

            Assert.Equal(createdItem.VehicleCarDetails.Model, reGotItem.VehicleCarDetails.Model);
            Assert.Equal(createdItem.VehicleCarDetails.BodyType, reGotItem.VehicleCarDetails.BodyType);
            Assert.Equal(createdItem.VehicleCarDetails.Doors, reGotItem.VehicleCarDetails.Doors);
        }


        /* TODO:
         * 
         * More tests here will be required to test the end-to-end functionality
         */
    }
}
