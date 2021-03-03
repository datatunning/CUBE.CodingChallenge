// <copyright file="TemperatureServiceTests.cs" company="Bruno DUVAL">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using CUBE.CodingChallenge.API.Models;
using CUBE.CodingChallenge.API.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace CUBE.CodingChallenge.API.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class TemperatureServiceTests
    {
        [Theory]
        // same unit conversion
        [InlineData(TemperatureUnit.Celsius, 0, TemperatureUnit.Celsius, 0)]
        [InlineData(TemperatureUnit.Fahrenheit, 0, TemperatureUnit.Fahrenheit, 0)]
        [InlineData(TemperatureUnit.Kelvin, 0, TemperatureUnit.Kelvin, 0)]

        // zero conversion-back
        [InlineData(TemperatureUnit.Celsius, 0, TemperatureUnit.Fahrenheit, 32)]
        [InlineData(TemperatureUnit.Fahrenheit, 32, TemperatureUnit.Celsius, 0)]
        [InlineData(TemperatureUnit.Celsius, 0, TemperatureUnit.Kelvin, 273.15)]
        [InlineData(TemperatureUnit.Kelvin, 273.15, TemperatureUnit.Celsius, 0)]
        [InlineData(TemperatureUnit.Fahrenheit, 0, TemperatureUnit.Kelvin, 255.3722222)]
        [InlineData(TemperatureUnit.Kelvin, 255, TemperatureUnit.Fahrenheit, -0.66999054)]

        // special cases
        [InlineData(TemperatureUnit.Celsius, -40, TemperatureUnit.Fahrenheit, -40)]
        [InlineData(TemperatureUnit.Fahrenheit, -40, TemperatureUnit.Celsius, -40)]
        public async void ShouldConvertTemperatureGivenWhenUnitAndValueAreGiven(TemperatureUnit fromUnit, float fromTemp,
            TemperatureUnit toUnit, float expectedTemp)
        {
            // Arrange
            var logger = Substitute.For<ILogger<ITemperatureService>>();
            var tempService = new TemperatureService(logger);

            // Act
            var temp = await tempService.ConvertAsync(fromUnit, fromTemp, toUnit);

            // Assert
            temp.Should().Be(expectedTemp);
        }

        [Fact]
        public async void ShouldThrowNotSupportedExceptionWhenInvalidUnitIsProvided()
        {
            // Arrange
            var logger = Substitute.For<ILogger<ITemperatureService>>();
            var tempService = new TemperatureService(logger);

            // Act
            var exception = await Record.ExceptionAsync(async () => await tempService.ConvertAsync((TemperatureUnit)4, 0, (TemperatureUnit)6));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<NotSupportedException>(exception);
        }
    }
}