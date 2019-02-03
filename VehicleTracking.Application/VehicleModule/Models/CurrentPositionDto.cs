using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.VehicleModule.Models
{
    public class CurrentPositionDto : PositionDto
    {
        [JsonProperty("vehicleId")]
        public Guid VehicleId { get; set; }
    }
}
