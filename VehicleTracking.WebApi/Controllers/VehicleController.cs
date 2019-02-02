using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleTracking.Application.VehicleModule.Commands;

namespace VehicleTracking.WebApi.Controllers
{
    public class VehicleController : BaseController
    {
        protected readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/vehicle")]
        public async Task<IActionResult> RegisterVehicle([FromBody] RegisterVehicleCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}