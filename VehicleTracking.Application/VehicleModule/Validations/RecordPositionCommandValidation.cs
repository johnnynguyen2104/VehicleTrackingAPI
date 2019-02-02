using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Commands;

namespace VehicleTracking.Application.VehicleModule.Validations
{
    public class RecordPositionCommandValidation : AbstractValidator<RecordPositionCommand>
    {
        public RecordPositionCommandValidation()
        {

        }
    }
}
