using System;
using Xunit;
using Moq;
using VehicleMicroservice.Core.Services;
using VehicleMicroservice.Data.Repositories;
using System.Collections.Generic;
using VehicleMicroservice.Contracts.Models;
using System.Threading.Tasks;

namespace VehicleMicroservice.UnitTests
{
    public class VehicleGetTests
    {
        // Given a set of existing products
        // When the complete product list is requested
        // Then a non-empty list should be returned
        [Fact]
        public async Task CanGetAllWithData()
        {
            var vehicleService = EstablishEnvironment(withVehicles: true);

            var result = await vehicleService.GetAll();

            Assert.NotEmpty(result);
        }

        // Given a empty set of existing products
        // When the complete product list is requested
        // Then an empty list should be returned
        [Fact]
        public async Task CanGetAllWithNoData()
        {
            var vehicleService = EstablishEnvironment(withVehicles: false);

            var result = await vehicleService.GetAll();

            Assert.Empty(result);
        }


        /* TODO:
         * Add a lot more unit tests here to make sure our validator works
         */

        protected VehicleService EstablishEnvironment(bool withVehicles)
        {
            // Establish the environment

            var productRepository = new Mock<IVehicleRepository>();

            var testVehicle = new Vehicle
            {
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = null,
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = 4,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };
            if (withVehicles)
            {
                productRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Vehicle>() { testVehicle });
            }
            else
            {
                productRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Vehicle>() { });
            }
            var vehicleService = new VehicleService(new VehicleValidator(),
                                                    productRepository.Object);

            return vehicleService;
        }

    }
}
