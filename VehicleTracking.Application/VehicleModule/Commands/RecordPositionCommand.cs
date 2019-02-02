using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Application.VehicleModule.Commands
{
    /// <summary>
    /// Using these properties below to make sure the api operate correct vehicle. 
    /// Without Device and Activated Code, we may face the problem with replaced device, or the device has been stolen by someone else.
    /// </summary>
    public class RecordPositionCommand : IRequest
    {
        public Guid VehicleId { get; set; }

        public string DeviceCode { get; set; }

        public string ActivatedCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
