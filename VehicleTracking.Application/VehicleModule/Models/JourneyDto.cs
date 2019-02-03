using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.VehicleModule.Models
{
    public class JourneyDto : IRequest
    {
        [JsonProperty("vehicleId")]
        public Guid VehicleId { get; set; }

        [JsonProperty("positions")]
        public IList<PositionDto> Positions { get; set; }
    }
}
