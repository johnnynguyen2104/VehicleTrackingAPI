using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Vehicle.Commands
{
    public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand>
    {

        public RegisterVehicleCommandHandler()
        {

        }

        public Task<Unit> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
