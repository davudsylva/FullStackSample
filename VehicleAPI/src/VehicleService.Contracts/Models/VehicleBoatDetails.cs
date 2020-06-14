using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMicroservice.Contracts.Models
{
    public class VehicleBoatDetails : VehicleDetails
    {
        public string Make { get; set; }
    }
}
