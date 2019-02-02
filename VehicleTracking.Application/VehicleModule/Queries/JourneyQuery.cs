using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Models;

namespace VehicleTracking.Application.VehicleModule.Queries
{
    public class JourneyQuery : IRequest<JourneyDto>
    {
    }
}
