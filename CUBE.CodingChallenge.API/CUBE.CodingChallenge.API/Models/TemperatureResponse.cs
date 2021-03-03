// <copyright file="TemperatureResponse.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.Annotations;

namespace CUBE.CodingChallenge.API.Models
{
    [ExcludeFromCodeCoverage]
    public class TemperatureResponse : TemperatureRequest
    {
        /// <summary>Initializes a new instance of the <see cref="TemperatureResponse" /> class.</summary>
        public TemperatureResponse() : base()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TemperatureRequest" /> class.</summary>
        /// <param name="fromUnit">The Temperature unit to convert from.</param>
        /// <param name="fromTemperature">The Temperature value to convert.</param>
        /// <param name="toUnit">The Temperature unit to convert to.</param>
        /// <param name="toTemperature">The converted Temperature value.</param>
        public TemperatureResponse(string fromUnit, float fromTemperature, string toUnit, float toTemperature) : base(
            fromUnit,
            fromTemperature, toUnit)
        {
            ToTemperature = toTemperature;
        }

        /// <summary>Initializes a new instance of the <see cref="TemperatureResponse" /> class.</summary>
        /// <param name="tempReq">The Temperature conversion request.</param>
        /// <param name="toTemperature">The converted Temperature value.</param>
        public TemperatureResponse(TemperatureRequest tempReq, float toTemperature) : base(tempReq.FromUnit,
            tempReq.FromTemperature, tempReq.ToUnit)
        {
            ToTemperature = toTemperature;
        }

        [SwaggerSchema("The converted Temperature value", ReadOnly = true)]
        public float ToTemperature { get; set; }
    }
}