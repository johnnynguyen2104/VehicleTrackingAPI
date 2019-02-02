using MediatR;
using System;
using VehicleTracking.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using VehicleTracking.Persistence.Interfaces;
using VehicleTracking.Application.Exceptions;

namespace VehicleTracking.Application.VehicleModule.Commands
{
    public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand>
    {
        private readonly IBaseRepository<Vehicle> _vehicleRepository;

        public RegisterVehicleCommandHandler(IBaseRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public Task<Unit> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Vehicle()
            {
                ActivatedCode = request.ActivatedCode,
                DeviceCode = request.DeviceCode,
                DeviceModel = request.DeviceModel,
                IsActive = request.IsActive,
                RegisteredName = request.RegisteredName,
                RegisteredPhone = request.RegisteredPhone
            };

            if (_vehicleRepository.Create(entity) == null)
            {
                throw new CreateVehicleFailException($"Fail to create a new vehicle with DeviceCode {request?.DeviceCode}");
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
