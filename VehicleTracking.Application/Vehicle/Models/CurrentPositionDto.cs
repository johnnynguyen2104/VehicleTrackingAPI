using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Vehicle.Models
{
    public class CurrentPositionDto : PositionDto
    {
        public Guid VehicleId { get; set; }
    }
}
