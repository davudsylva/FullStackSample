using VehicleMicroservice.Contracts.Models;

namespace VehicleMicroservice.Core.Services
{
    public interface IVehicleValidator
    {
        (bool isOk, string reason) ValidateVehicle(Vehicle vehicle);
    }
}
