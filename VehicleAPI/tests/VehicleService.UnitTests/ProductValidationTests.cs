using System;
using Xunit;
using ProductMicroservice.Core.Services;
using ProductMicroservice.Contracts.Models;

namespace ProductMicroservice.UnitTests
{
    public class ProductValidationTests
    {
        // Given a vehicle with a null make
        // When the vehicle is validated
        // Then the a failure status should be returned
        [Fact]
        public void CanValidateNullDescription()
        {
            var validator = new VehicleValidator();

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
            var result = validator.ValidateVehicle(testVehicle);

            Assert.False(result.isOk);
        }

        // Given a valid vehicle
        // When the vehicle is validated
        // Then the a failure status should not be returned
        [Fact]
        public void CanValidateValidVehicle()
        {
            var validator = new VehicleValidator();

            var testVehicle = new Vehicle
            {
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
            var result = validator.ValidateVehicle(testVehicle);

            Assert.True(result.isOk);
        }

        // Given a vehicle with various door values
        // When the product is validated
        // Then the appropriate failure status should be returned
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        public void CanValidateDoors(int doors, bool expectedValid)
        {
            var validator = new VehicleValidator();

            var testVehicle = new Vehicle
            {
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = "Ford",
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = doors,
                    Wheels = 3,
                    BodyType = "Sedan"
                }
            };
            var result = validator.ValidateVehicle(testVehicle);

            Assert.Equal(expectedValid, result.isOk);
        }

        // Given a vehicle with various wheel values
        // When the product is validated
        // Then the appropriate failure status should be returned
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, false)]
        public void CanValidateWheels(int wheels, bool expectedValid)
        {
            var validator = new VehicleValidator();

            var testVehicle = new Vehicle
            {
                VehicleCarDetails = new VehicleCarDetails()
                {
                    Make = "Ford",
                    Model = "Falcon",
                    Engine = "4.0L Inline 6",
                    Doors = 4,
                    Wheels = wheels,
                    BodyType = "Sedan"
                }
            };
            var result = validator.ValidateVehicle(testVehicle);

            Assert.Equal(expectedValid, result.isOk);
        }

        /* TODO:
         * Add a lot more unit tests here to make sure our validator works
         */
    }
}
