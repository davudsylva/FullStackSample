using ProductMicroservice.Contracts.Models;
using ProductMicroservice.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public class VehicleService : IVehicleService
    {
        IVehicleRepository _vehicleRepository;
        IVehicleValidator _productValidator;

        public VehicleService(IVehicleValidator productValidator, IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            _productValidator = productValidator;
        }

        public async Task<IEnumerable<Vehicle>> GetByName()
        {
            return await _vehicleRepository.GetAll();
        }

        public async Task<Vehicle> GetById(Guid productId)
        {
            return await _vehicleRepository.GetById(productId);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _vehicleRepository.GetAll();
        }


        public async Task Update(Vehicle product)
        {
            await _vehicleRepository.Update(product);
        }

        public async Task<Vehicle> Create(Vehicle product)
        {
            var validation = _productValidator.ValidateVehicle(product);
            if (!validation.isOk)
            {
                throw new Exception($"Validation Error: {validation.reason}");
            }
            product.Id = Guid.NewGuid();
            if (product.VehicleCarDetails != null)
            {
                product.VehicleCarDetails.DetailId = Guid.NewGuid();
                product.VehicleCarDetails.VehicleId = product.Id.Value;
            }
            await _vehicleRepository.Create(product);
            return product;
        }

        public async Task DeleteById(Guid productId)
        {
            await _vehicleRepository.DeleteById(productId);
        }
    }
}
