using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductMicroservice.Contracts.Models
{
    public class VehicleBoatDetails : VehicleDetails
    {
        public string Make { get; set; }
    }
}
