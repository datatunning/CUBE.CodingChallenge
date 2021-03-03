// <copyright file="TemperatureRequest.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.Annotations;

namespace CUBE.CodingChallenge.API.Models
{
    [ExcludeFromCodeCoverage]
    public class TemperatureRequest
    {
        /// <summary>Initializes a new instance of the <see cref="TemperatureRequest" /> class.</summary>
        public TemperatureRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TemperatureRequest" /> class.</summary>
        /// <param name="fromUnit">The Temperature unit to convert from.</param>
        /// <param name="fromTemperature">The Temperature value to convert.</param>
        /// <param name="toUnit">The Temperature unit to convert to.</param>
        public TemperatureRequest(string fromUnit, float fromTemperature, string toUnit)
        {
            FromUnit = fromUnit;
            FromTemperature = fromTemperature;
            ToUnit = toUnit;
        }

        [SwaggerSchema("The Temperature unit to convert from", ReadOnly = true)]
        public string FromUnit { get; set; }

        [SwaggerSchema("The Temperature value to convert", ReadOnly = true)]
        public float FromTemperature { get; set; }

        [SwaggerSchema("The Temperature unit to convert to", ReadOnly = true)]
        public string ToUnit { get; set; }
    }
}