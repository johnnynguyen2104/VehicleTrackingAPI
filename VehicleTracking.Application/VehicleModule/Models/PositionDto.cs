using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.VehicleModule.Models
{
    public class PositionDto
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("positionDateTime")]
        public DateTime PositionDateTime { get; set; }
    }
}
