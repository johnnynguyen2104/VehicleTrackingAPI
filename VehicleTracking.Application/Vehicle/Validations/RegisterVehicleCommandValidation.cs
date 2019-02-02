using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.Vehicle.Commands;

namespace VehicleTracking.Application.Vehicle.Validations
{
    public class RegisterVehicleCommandValidation: AbstractValidator<RegisterVehicleCommand>
    {
        public RegisterVehicleCommandValidation()
        {

        }
    }
}
