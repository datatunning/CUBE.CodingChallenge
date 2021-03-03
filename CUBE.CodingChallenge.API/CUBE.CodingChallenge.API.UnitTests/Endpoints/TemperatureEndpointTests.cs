// <copyright file="TemperatureEndpointTests.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using CUBE.CodingChallenge.API.Endpoints;
using CUBE.CodingChallenge.API.Models;
using CUBE.CodingChallenge.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace CUBE.CodingChallenge.API.UnitTests.Endpoints
{
    [ExcludeFromCodeCoverage]
    public class TemperatureEndpointTests
    {
        [Theory]
        [InlineData("Fahrenheit", 32, "Celsius", 0)]
        [InlineData("Celsius", 0, "Kelvin", 273.15)]
        [InlineData("Kelvin", 273.15, "Celsius", 0)]
        [InlineData("Celsius", 0, "Fahrenheit", 32)]
        public async void ShouldReturnConvertedTemperature(string fromUnit, float fromTemp,
            string toUnit, float toTemp)
        {
            // Arrange
            var tsLogger = Substitute.For<ILogger<ITemperatureService>>();
            var tempService = new TemperatureService(tsLogger);

            var epLogger = Substitute.For<ILogger<TemperatureEndpoint>>();
            var endpoint = new TemperatureEndpoint(epLogger, tempService);

            // Act
            var response =
                (await endpoint.HandleAsync(new TemperatureRequest(fromUnit, fromTemp, toUnit))).Result as
                OkObjectResult;

            // Assert
            response?.StatusCode.Should().Be(StatusCodes.Status200OK);
            response?.Value.Should().BeOfType<TemperatureResponse>();
            var tempResponse = response?.Value as TemperatureResponse;
            tempResponse.Should().NotBeNull();
            tempResponse?.ToTemperature.Should().Be(toTemp);
        }
        
        [Theory]
        [InlineData("NightyDegreeFahrenheit", "Celsius")]
        [InlineData("Celsius", "KelvinKlein")]
        [InlineData("Kelvin", "CelsiusCesar")]
        [InlineData("ExCelsius", "Fahrenheit")]
        public async void ShouldReturnBadRequestWhenInvalidUnitProvided(string fromUnit, string toUnit)
        {
            // Arrange
            var tsLogger = Substitute.For<ILogger<ITemperatureService>>();
            var tempService = new TemperatureService(tsLogger);

            var epLogger = Substitute.For<ILogger<TemperatureEndpoint>>();
            var endpoint = new TemperatureEndpoint(epLogger, tempService);

            // Act
            var response =
                (await endpoint.HandleAsync(new TemperatureRequest(fromUnit, 0, toUnit))).Result as OkObjectResult;

            // Assert
            response?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response?.Value.Should().BeOfType<TemperatureResponse>();
            var tempResponse = response?.Value as TemperatureResponse;
            tempResponse.Should().BeNull();
        }
        
        [Theory]
        [InlineData("Reaumure", "Celsius")]
        [InlineData("Kelvin", "Reaumure")]
        public async void ShouldReturnNotImplementedWhenUnitProvidedHasNoImplementation(string fromUnit, string toUnit)
        {
            // Arrange
            var tsLogger = Substitute.For<ILogger<ITemperatureService>>();
            var tempService = new TemperatureService(tsLogger);

            var epLogger = Substitute.For<ILogger<TemperatureEndpoint>>();
            var endpoint = new TemperatureEndpoint(epLogger, tempService);

            // Act
            var response =
                (await endpoint.HandleAsync(new TemperatureRequest(fromUnit, 0, toUnit))).Result as OkObjectResult;

            // Assert
            response?.StatusCode.Should().Be(StatusCodes.Status501NotImplemented);
            response?.Value.Should().BeOfType<TemperatureResponse>();
            var tempResponse = response?.Value as TemperatureResponse;
            tempResponse.Should().BeNull();
        }
        
        [Fact]
        public async void ShouldReturnInternalServerErrorOnUnhandledException()
        {
            // Arrange
            TemperatureService tempService = null;

            var epLogger = Substitute.For<ILogger<TemperatureEndpoint>>();
            var endpoint = new TemperatureEndpoint(epLogger, tempService);

            // Act
            var response =
                (await endpoint.HandleAsync(new TemperatureRequest("Kelvin", 0, "Celsius"))).Result as OkObjectResult;

            // Assert
            response?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            response?.Value.Should().BeOfType<TemperatureResponse>();
            var tempResponse = response?.Value as TemperatureResponse;
            tempResponse.Should().BeNull();
        }

    }
}