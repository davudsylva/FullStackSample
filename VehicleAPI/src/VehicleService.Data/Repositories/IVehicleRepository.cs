using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.Data.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(Guid vehicleId);
        Task Update(Vehicle vehicle);
        Task Create(Vehicle vehicle);
        Task DeleteById(Guid vehicleId);
    }
}
