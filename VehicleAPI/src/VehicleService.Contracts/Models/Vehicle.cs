using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMicroservice.Contracts.Models
{
    public class Vehicle
    {
        public Guid? Id { get; set; }
        public string VehicleType { get; set; }
        public VehicleCarDetails VehicleCarDetails { get; set; }
        public VehicleBoatDetails VehicleBoatDetails { get; set; }
    }
}
