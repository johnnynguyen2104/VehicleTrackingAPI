using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Application.Vehicle.Models;

namespace VehicleTracking.Application.Vehicle.Queries
{
    public class JourneyQueryHandler : IRequestHandler<JourneyQuery, JourneyDto>
    {
        public Task<JourneyDto> Handle(JourneyQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
