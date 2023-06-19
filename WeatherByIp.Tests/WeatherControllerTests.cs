using Moq;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WeatherByIp.Controllers;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Tests
{
    public class WeatherControllerTests
    {
        private WeatherController _weatherController;
        private Mock<IApiReturnInfoService> _apiReturnInfoService;
        private Mock<IValidationOfIpAddress> _validationOfIpAddress;
        private string _validIp = "185.182.71.38";

        [SetUp]
        public void Setup()
        {
            _apiReturnInfoService = new Mock<IApiReturnInfoService>();
            _apiReturnInfoService
                .Setup(l => l.GetCurrentWeather(defaultReturnInfo.Ip))
                .ReturnsAsync(defaultReturnInfo);

            _validationOfIpAddress = new Mock<IValidationOfIpAddress>();
            _validationOfIpAddress
                .Setup(i => i.IsValidIpAddress(defaultReturnInfo.Ip))
                .Returns(IPAddress.Parse(defaultReturnInfo.Ip));
            _validationOfIpAddress
                .Setup(i => i.IsValidIpAddress(_validIp))
                .Returns(IPAddress.Parse(_validIp));

            _weatherController = new WeatherController(_apiReturnInfoService.Object, _validationOfIpAddress.Object);
        }

        [Test]
        public async Task CheckWeather_LocationIp_Ok()
        {
            var ip = defaultReturnInfo.Ip;

            var result = await _weatherController.CheckWeather(ip);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task CheckWeather_ValidIp_NotFound()
        {
            var ip = _validIp;

            var result = await _weatherController.CheckWeather(ip);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task CheckWeather_InvalidIp_BadRequest()
        {
            var ip = "someIp";

            var result = await _weatherController.CheckWeather(ip);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        private ApiReturnInfo defaultReturnInfo = new ApiReturnInfo
        (
            new Location
            {
                Id = 111,
                City = "Riga",
                Country = "LV",
                Ip = "196.196.53.51",
                Latitude = 56.946M,
                Longitude = 24.1059M,
                EntryDateTime = DateTime.Now
            },
            new Weather
            {
                Latitude = 56.946M,
                Longitude = 24.1059M,
                Temperature = 19,
                Windspeed = 20,
                WindDirection = 21,
                WeatherState = "Mainly clear"
            }
        );
    }
}
