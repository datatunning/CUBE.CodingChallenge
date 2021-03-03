// <copyright file="TemperatureService.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Threading.Tasks;
using CUBE.CodingChallenge.API.Models;
using Microsoft.Extensions.Logging;

namespace CUBE.CodingChallenge.API.Services
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ILogger<ITemperatureService> _logger;

        /// <summary>Initializes a new instance of the <see cref="TemperatureService" /> class.</summary>
        /// <param name="logger">The logger.</param>
        public TemperatureService(ILogger<ITemperatureService> logger)
        {
            _logger = logger;
        }

        /// <summary>Converts the specified from unit.</summary>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="fromTemp">From temporary.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<float> ConvertAsync(TemperatureUnit fromUnit, float fromTemp, TemperatureUnit toUnit)
        {
            var toTemp = fromUnit switch
            {
                TemperatureUnit.Celsius when toUnit == TemperatureUnit.Celsius => fromTemp,
                // Celsius to Fahrenheit  F = 9 / 5(C) + 32
                TemperatureUnit.Celsius when toUnit == TemperatureUnit.Fahrenheit => 9.0f / 5.0f * fromTemp + 32f,
                // Celsius to Kelvin K = C + 273
                TemperatureUnit.Celsius when toUnit == TemperatureUnit.Kelvin => fromTemp + 273.15f,

                // Fahrenheit to Celsius  C = 5 / 9(F - 32)
                TemperatureUnit.Fahrenheit when toUnit == TemperatureUnit.Celsius => 5.0f / 9.0f * (fromTemp - 32f),
                TemperatureUnit.Fahrenheit when toUnit == TemperatureUnit.Fahrenheit => fromTemp,
                // Fahrenheit to Kelvin K = 5 / 9(F - 32) + 273
                TemperatureUnit.Fahrenheit when toUnit == TemperatureUnit.Kelvin => (fromTemp - 32f) * 5.0f / 9.0f + 273.15f,

                // Kelvin to Celsius  C = K - 273
                TemperatureUnit.Kelvin when toUnit == TemperatureUnit.Celsius => fromTemp - 273.15f,
                // Kelvin to Fahrenheit  F = 9 / 5(K - 273) + 32
                TemperatureUnit.Kelvin when toUnit == TemperatureUnit.Fahrenheit => (((fromTemp - 273.15f) * 9.0f) / 5.0f) + 32f,
                TemperatureUnit.Kelvin when toUnit == TemperatureUnit.Kelvin => fromTemp,

                _ => throw new NotSupportedException("The request is not supported.")
            };

            _logger.LogInformation($"Converting {fromTemp} {fromUnit} to {toTemp} {toUnit}");
            return Task.FromResult(toTemp);
        }
    }
}