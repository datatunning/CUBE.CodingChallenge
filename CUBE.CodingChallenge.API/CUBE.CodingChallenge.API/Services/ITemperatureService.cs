// <copyright file="ITemperatureService.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Threading.Tasks;
using CUBE.CodingChallenge.API.Models;

namespace CUBE.CodingChallenge.API.Services
{
    /// <summary>
    ///   Interface for the Temperature Conversion Service.
    /// </summary>
    public interface ITemperatureService
    {
        /// <summary>Converts the specified from unit.</summary>
        /// <param name="fromUnit">From unit.</param>
        /// <param name="fromTemp">From temporary.</param>
        /// <param name="toUnit">To unit.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<float> ConvertAsync(TemperatureUnit fromUnit, float fromTemp, TemperatureUnit toUnit);
    }
}