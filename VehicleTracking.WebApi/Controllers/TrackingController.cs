using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleTracking.Application.VehicleModule.Commands;
using VehicleTracking.Application.VehicleModule.Queries;

namespace VehicleTracking.WebApi.Controllers
{

    public class TrackingController : BaseController
    {

        protected readonly ILogger<TrackingController> _logger;

        public TrackingController(ILogger<TrackingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/tracking")]
        public async Task<IActionResult> RecordPosition([FromBody] RecordPositionCommand recordCommand)
        {
            await Mediator.Send(recordCommand);
            return NoContent();
        }

        [Route("api/tracking/{vehicleId}/{activatedCode}/{deviceCode}")]
        public async Task<IActionResult> GetCurrentPosition([FromRoute] CurrentPositionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [Route("api/tracking/{vehicleId}/{activatedCode}/{deviceCode}/journey/{fromDateTime}/{toDateTime}")]
        public async Task<IActionResult> GetJourney([FromRoute] JourneyQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
