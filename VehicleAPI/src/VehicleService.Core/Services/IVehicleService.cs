using VehicleMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleMicroservice.Core.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(Guid id);
        Task Update(Vehicle vehicle);
        Task<Vehicle> Create(Vehicle vehicle);
        Task DeleteById(Guid id);
    }
}
