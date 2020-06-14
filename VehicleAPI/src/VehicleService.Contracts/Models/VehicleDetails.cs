using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMicroservice.Contracts.Models
{
    public class VehicleDetails
    {
        [JsonIgnore]
        public Guid DetailId { get; set; }

        [JsonIgnore]
        public Guid VehicleId { get; set; }

    }
}
