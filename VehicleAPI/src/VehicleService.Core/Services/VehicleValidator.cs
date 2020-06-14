using ProductMicroservice.Contracts.Models;
using ProductMicroservice.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public class VehicleValidator : IVehicleValidator
    {
        // TODO: When non-car vehicles are required, a more elegant solution here 
        //       would be nice. 
        public (bool isOk, string reason) ValidateVehicle(Vehicle vehicle)
        {
            if (String.IsNullOrEmpty(vehicle.VehicleType))
            {
                return (false, "Empty VehicleType");
            }
            if (vehicle.VehicleCarDetails == null && vehicle.VehicleBoatDetails == null)
            {
                return (false, "Vehicle details not passed");

            }
            if (vehicle.VehicleCarDetails != null && vehicle.VehicleBoatDetails != null)
            {
                return (false, "Conflicting vehicle details  passed");
            }

            if (vehicle.VehicleCarDetails != null)
            {
                var detailsValidation = ValidateCarDetails(vehicle.VehicleCarDetails);
                if (!detailsValidation.isOk)
                {
                    return detailsValidation;
                }
            }
            if (vehicle.VehicleBoatDetails != null)
            {
                var detailsValidation = ValidateBoatDetails(vehicle.VehicleBoatDetails);
                if (!detailsValidation.isOk)
                {
                    return detailsValidation;
                }
            }
            // TODO: Add more checks here

            return (true, null);
        }

        public (bool isOk, string reason) ValidateCarDetails(VehicleCarDetails details)
        {
            if (details.Doors < 1)
            {
                return (false, "Invalid number of doors");
            }

            if (details.Wheels < 1)
            {
                return (false, "Invalid number of wheels");
            }

            // TODO: Add more checks here

            return (true, null);
        }


        public (bool isOk, string reason) ValidateBoatDetails(VehicleBoatDetails details)
        {
            // TODO: Add checks here when implemented

            return (true, null);
        }

    }
}
