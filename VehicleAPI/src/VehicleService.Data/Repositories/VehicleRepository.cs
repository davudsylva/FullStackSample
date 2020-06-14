using Dapper;
using VehicleMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VehicleMicroservice.Data.Repositories
{
    public class VehicleRepository : Repository, IVehicleRepository
    {
        public VehicleRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            // Poorman's ORM. The repo should sue a better store - NoSQL or at least used SQL Servers "as json" construct
            using (var connection = CreateConnection())
            {
                var baseDir = System.IO.Directory.GetCurrentDirectory();
                await connection.OpenAsync();
                var dbVehicleResult = await connection.QueryAsync<Vehicle>($"select * from Vehicle");
                var dbCarResult = await connection.QueryAsync<VehicleCarDetails>($"select * from VehicleCarDetail");
                var dbBoatResult = await connection.QueryAsync<VehicleBoatDetails>($"select * from VehicleBoatDetail");
                var vehicles = dbVehicleResult.AsList();
                var carDetails = dbCarResult.AsList();
                var boatDetails = dbBoatResult.AsList();

                foreach (var carDetail in carDetails)
                {
                    vehicles.Find(x => x.Id == carDetail.VehicleId).VehicleCarDetails = carDetail;
                }
                foreach (var boatDetail in boatDetails)
                {
                    vehicles.Find(x => x.Id == boatDetail.VehicleId).VehicleBoatDetails = boatDetail;
                }
                // TODO: Inlcude other vehicle types

                return vehicles;
            }
        }

        public async Task<Vehicle> GetById(Guid vehicleId)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();
                    var dbVehicleResult = await connection.QueryAsync<Vehicle>($"select * from Vehicle where id = '{vehicleId}' collate nocase");

                    var vehicle = dbVehicleResult.FirstOrDefault();
                    if (vehicle != null)
                    {
                        var dbCarResult = await connection.QueryAsync<VehicleCarDetails>($"select * from VehicleCarDetail where VehicleId = '{vehicleId}' collate nocase");
                        var dbBoatResult = await connection.QueryAsync<VehicleBoatDetails>($"select * from VehicleBoatDetail where VehicleId = '{vehicleId}' collate nocase");
                        vehicle.VehicleCarDetails = dbCarResult.AsList().FirstOrDefault();
                        vehicle.VehicleBoatDetails = dbBoatResult.AsList().FirstOrDefault();
                    }
                    return vehicle;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Update(Vehicle vehicle)
        {
            // Note: A vehicle cannot change from a car to a boat.
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync($"update Vehicle set VehicleType = '{vehicle.VehicleType}' where id = '{vehicle.Id}' collate nocase");
                if (vehicle.VehicleCarDetails != null)
                {
                    await connection.ExecuteAsync($@"update VehicleCarDetail 
                                                     set Make = '{vehicle.VehicleCarDetails.Make}',
                                                         Model = '{vehicle.VehicleCarDetails.Model}',
                                                         Engine = '{vehicle.VehicleCarDetails.Engine}',
                                                         Doors = {vehicle.VehicleCarDetails.Doors},
                                                         Wheels = {vehicle.VehicleCarDetails.Wheels},
                                                         BodyType = '{vehicle.VehicleCarDetails.BodyType}' 
                                                     where DetailId= '{vehicle.VehicleCarDetails.DetailId}' and VehicleId = '{vehicle.VehicleCarDetails.VehicleId}'");
                }
                if (vehicle.VehicleBoatDetails != null)
                {
                    await connection.ExecuteAsync($@"update VehicleCarDetail 
                                                     set Make = '{vehicle.VehicleBoatDetails.Make}',
                                                     where DetailId= '{vehicle.VehicleBoatDetails.DetailId}' and VehicleId = '{vehicle.VehicleBoatDetails.VehicleId}'");
                }
            }
        }

        public async Task Create(Vehicle vehicle)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync($"insert into Vehicle (Id, VehicleType) values ('{vehicle.Id}', '{vehicle.VehicleType}')");
                if (vehicle.VehicleCarDetails != null)
                {
                    await connection.ExecuteAsync($@"insert into VehicleCarDetail (DetailId, VehicleId, Make, Model, Engine, Doors, Wheels, BodyType) 
                                                     values ('{vehicle.VehicleCarDetails.DetailId}',
                                                             '{vehicle.VehicleCarDetails.VehicleId}',
                                                             '{vehicle.VehicleCarDetails.Make}', 
                                                             '{vehicle.VehicleCarDetails.Model}', 
                                                             '{vehicle.VehicleCarDetails.Engine}', 
                                                              {vehicle.VehicleCarDetails.Doors},
                                                              {vehicle.VehicleCarDetails.Wheels},
                                                             '{vehicle.VehicleCarDetails.BodyType}')");
                }
                if (vehicle.VehicleBoatDetails != null)
                {
                    await connection.ExecuteAsync($@"insert into VehicleBoatDetail (Id, VehicleId, Make) 
                                                     values ('{vehicle.VehicleCarDetails.DetailId}',
                                                             '{vehicle.VehicleCarDetails.VehicleId}',
                                                             '{vehicle.VehicleCarDetails.Make}')");
                }
            }
        }

        public async Task DeleteById(Guid vehicleId)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync($"delete from VehicleCarDetail where VehicleId = '{vehicleId}' collate nocase");
                await connection.ExecuteAsync($"delete from VehicleBoatDetail where VehicleId = '{vehicleId}' collate nocase");
                await connection.ExecuteAsync($"delete from Vehicle where id = '{vehicleId}' collate nocase");
            }
        }
    }
}
