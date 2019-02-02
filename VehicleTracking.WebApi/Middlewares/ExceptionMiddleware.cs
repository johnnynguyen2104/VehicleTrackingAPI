using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracking.Common;
using VehicleTracking.Persistence.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleTracking.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next
            , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errorResult = new ErrorDetails() { Reason = $"Message: {ex.Message}, Source: {ex.Source}, Method: {ex.TargetSite}"
                    , ErrorCode = HttpStatusCode.InternalServerError.ToString() }.ToString();

                _logger.LogError(errorResult);

                await httpContext.Response.WriteAsync(errorResult);
            }
        }

      
    }
}
