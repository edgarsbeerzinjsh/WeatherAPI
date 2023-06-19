using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using FluentAssertions;
using WeatherByIp.Controllers;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using Microsoft.AspNetCore.Http;

namespace WeatherByIp.Tests
{
    public class GetWeatherControllerTests
    {
        private GetWeatherController _getWeatherController;
        private Mock<IApiReturnInfoService> _apiReturnInfoService;
        private DefaultHttpContext _defaultHttpContext;

        [SetUp]
        public void Setup()
        {
            _apiReturnInfoService = new Mock<IApiReturnInfoService>();
            _defaultHttpContext = new DefaultHttpContext();
            _defaultHttpContext.Connection.RemoteIpAddress = IPAddress.Parse(defaultReturnInfo.Ip);
        }

        [Test]
        public async Task CheckWeather_ValidIp_Ok()
        {
            _apiReturnInfoService
                .Setup(l => l.GetCurrentWeather(defaultReturnInfo.Ip))
                .ReturnsAsync(defaultReturnInfo);

            _getWeatherController = new GetWeatherController(_apiReturnInfoService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _defaultHttpContext
                }
            };


            var result = await _getWeatherController.GetWeather();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task CheckWeather_NoIp_NotFound()
        {
            _getWeatherController = new GetWeatherController(_apiReturnInfoService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _defaultHttpContext
                }
            };

            var result = await _getWeatherController.GetWeather();

            result.Should().BeOfType<NotFoundObjectResult>();
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
