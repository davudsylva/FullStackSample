using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductMicroservice.Core.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(Guid id);
        Task Update(Vehicle product);
        Task<Vehicle> Create(Vehicle product);
        Task DeleteById(Guid id);
    }
}
