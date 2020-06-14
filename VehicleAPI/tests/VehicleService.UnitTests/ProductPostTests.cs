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
    public class ProductPostTests
    {
        // Given an invalid vehicle
        // When we try to save it
        // Then we expect an exception 
        //  and for the repository's create method to be not called.
        [Fact]
        public async Task CantCreateInvalidProduct()
        {
            var testEnvironment = EstablishEnvironment();

            var testVehicle = new Vehicle
            {
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = null,
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = -4,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };

            await Assert.ThrowsAsync<Exception>(() => testEnvironment.vehicleService.Create(testVehicle));

            testEnvironment.productRepositoryMock.Verify(x => x.Create(It.IsAny<Vehicle>()), Times.Never);
        }

        // Given a valid product
        // When the we try and save it
        // Then we expect the repositry to be called to save it
        //  and a valid response to be returned
        [Fact]
        public async Task CanCreateValidProduct()
        {
            var testEnvironment = EstablishEnvironment();

            var testVehicle = new Vehicle
            {
                VehicleType = "Car",
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

            var result = await testEnvironment.vehicleService.Create(testVehicle);

            Assert.NotNull(result);

            testEnvironment.productRepositoryMock.Verify(x => x.Create(It.IsAny<Vehicle>()), Times.Once);
        }


        /* TODO:
         * Add a lot more unit tests here 
         */

        protected (VehicleService vehicleService, Mock<IVehicleRepository> productRepositoryMock) EstablishEnvironment()
        {
            // Establish the environment

            var vehicleRepositoryMock = new Mock<IVehicleRepository>();

            vehicleRepositoryMock.Setup(x => x.Create(It.IsAny<Vehicle>()));

            var vehicleService = new VehicleService(new VehicleValidator(),
                                                    vehicleRepositoryMock.Object);

            return (vehicleService, vehicleRepositoryMock);
        }

    }
}
