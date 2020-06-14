using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductMicroservice.Contracts.Models
{
    public class VehicleCarDetails : VehicleDetails
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public int Doors { get; set; }
        public int Wheels { get; set; }
        public string BodyType { get; set; }
    }
}
