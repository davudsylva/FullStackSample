using ProductMicroservice.Contracts.Models;

namespace ProductMicroservice.Core.Services
{
    public interface IVehicleValidator
    {
        (bool isOk, string reason) ValidateVehicle(Vehicle product);
    }
}
