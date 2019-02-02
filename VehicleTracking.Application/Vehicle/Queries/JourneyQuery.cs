using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.Vehicle.Models;

namespace VehicleTracking.Application.Vehicle.Queries
{
    public class JourneyQuery : IRequest<JourneyDto>
    {
    }
}
