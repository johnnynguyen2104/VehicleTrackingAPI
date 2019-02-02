using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracking.Application.VehicleModule.Models;

namespace VehicleTracking.Application.VehicleModule.Queries
{
    /// <summary>
    /// Using these properties below to make sure the api returning the correct data 
    /// to correct device. Without Device and Activated Code, we may face the problem with replaced device, or the device has been stolen by someone else.
    /// </summary>
    public class CurrentPositionQuery : IRequest<CurrentPositionDto>
    {
        public Guid VehicleId { get; set; }

        public string DeviceCode { get; set; }

        public string ActivatedCode { get; set; }
    }
}
