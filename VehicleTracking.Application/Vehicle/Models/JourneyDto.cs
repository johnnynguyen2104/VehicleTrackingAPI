using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.Vehicle.Models
{
    public class JourneyDto : IRequest
    {
        public Guid VehicleId { get; set; }

        public IList<PositionDto> Positions { get; set; }
    }
}
