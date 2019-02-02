using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTracking.Application.Vehicle.Commands
{
    public class RecordPositionCommandHandler : IRequestHandler<RecordPositionCommand>
    {
        public RecordPositionCommandHandler()
        {

        }

        public Task<Unit> Handle(RecordPositionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
