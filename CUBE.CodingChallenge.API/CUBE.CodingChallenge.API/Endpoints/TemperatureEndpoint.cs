// <copyright file="TemperatureEndpoint.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using CUBE.CodingChallenge.API.Models;
using CUBE.CodingChallenge.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace CUBE.CodingChallenge.API.Endpoints
{
    public class
        TemperatureEndpoint : BaseAsyncEndpoint.WithRequest<TemperatureRequest>.WithResponse<TemperatureResponse>
    {
        private readonly ILogger<TemperatureEndpoint> _logger;
        private readonly ITemperatureService _temperatureService;

        public TemperatureEndpoint(ILogger<TemperatureEndpoint> logger, ITemperatureService temperatureService)
        {
            _logger = logger;
            _temperatureService = temperatureService;
            _logger.LogInformation(
                $"Initializing Temperature endpoint");
        }
        
        [HttpGet("/convert")]
        [SwaggerOperation(
            Summary = "Convert the temperature from one unit to another",
            Description = "Convert between Celsius, Kelvin and Fahrenheit temperature",
            OperationId = "Temperature.Convert",
            Tags = new[] {nameof(TemperatureEndpoint)})
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<TemperatureResponse>> HandleAsync(
            [FromQuery] [SwaggerParameter("The Temperature conversion request")]
            TemperatureRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received request to convert ");
            try
            {
                if (!Enum.TryParse(typeof(TemperatureUnit), request.FromUnit, true, out var fromUnit))
                {
                    var msg = $"Bad request. The {request.FromUnit} is not a recognized unit.";
                    _logger.LogError(msg, request);
                    return Problem(msg, null, (int)HttpStatusCode.BadRequest, "Bad request");
                }
                if(!Enum.TryParse(typeof(TemperatureUnit), request.ToUnit, true, out var toUnit))
                {
                    var msg = $"Bad request. The {request.ToUnit} is not a recognized unit.";
                    _logger.LogError(msg, request);
                    return Problem(msg, null, (int)HttpStatusCode.BadRequest, "Bad request");
                }

                var toTemp = await _temperatureService.ConvertAsync((TemperatureUnit) fromUnit, request.FromTemperature,
                    (TemperatureUnit) toUnit);

                return Ok(new TemperatureResponse(request, toTemp));
            }
            catch (NotSupportedException nsEx)
            {
                _logger.LogError(nsEx, "Not supported request", request);
                return Problem(nsEx.Message, null, (int) HttpStatusCode.NotImplemented, "Not supported request");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error", request);
                return Problem(ex.Message, null, (int) HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}