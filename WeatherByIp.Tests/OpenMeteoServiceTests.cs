using Moq;
using Refit;
using System.Net;
using FluentAssertions;
using WeatherByIp.Core.Models;
using WeatherByIp.Services;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Tests
{
    public class OpenMeteoServiceTests
    {
        private OpenMeteoService _openMeteoService;
        private Mock<IWeatherAPI> _weatherApi;
        private Mock<IOpenMeteoDataValidation> _validator;

        [SetUp]
        public void Setup()
        {
            _weatherApi = new Mock<IWeatherAPI>();
            _validator = new Mock<IOpenMeteoDataValidation>();
        }

        [Test]
        public async Task GetWeatherFromCoordinates_ValidCoordinates_WeatherReturned()
        {
            var testLatitude = 56.946M;
            var testLongitude = 24.1059M;

            var openMeteoTest = new OpenMeteoData()
            {
                latitude = testLatitude,
                longitude = testLongitude,
                current_weather = new WeatherParameters()
                {
                    temperature = 19,
                    windspeed = 20,
                    winddirection = 21,
                    weathercode = 1
                }
            };
            var apiData = OpenMeteoData(openMeteoTest);

            var defaultWeather = new Weather()
            {
                Latitude = 56.946M,
                Longitude = 24.1059M,
                Temperature = 19,
                Windspeed = 20,
                WindDirection = 21,
                WeatherState = "Mainly clear"
            };

            _weatherApi
                .Setup(api => api.GetApiWeather(testLatitude, testLongitude))
                .ReturnsAsync(apiData);
            _validator
                .Setup(e => e.IsValidWeatherData(apiData))
                .Returns(true);

            _openMeteoService = new OpenMeteoService(_weatherApi.Object, new[] { _validator.Object });

            var result = await _openMeteoService.GetWeatherFromCoordinates(testLatitude, testLongitude);

            result.Should().BeEquivalentTo(defaultWeather, options => options.Excluding(t => t.EntryDateTime));
        }

        [Test]
        public async Task GetWeatherFromCoordinates_InvalidValidation_NullReturned()
        {
            var testLatitude = 56.946M;
            var testLongitude = 24.1059M;

            var openMeteoTest = new OpenMeteoData()
            {
                latitude = testLatitude,
                longitude = testLongitude,
                current_weather = new WeatherParameters() { }
            };
            var apiData = OpenMeteoData(openMeteoTest);

            _weatherApi
                .Setup(api => api.GetApiWeather(testLatitude, testLongitude))
                .ReturnsAsync(apiData);
            _validator
                .Setup(e => e.IsValidWeatherData(apiData))
                .Returns(false);

            _openMeteoService = new OpenMeteoService(_weatherApi.Object, new[] { _validator.Object });

            var result = await _openMeteoService.GetWeatherFromCoordinates(testLatitude, testLongitude);

            result.Should().BeNull();
        }

        [Test]
        public async Task GetWeatherFromCoordinates_InvalidResponse_NullReturned()
        {
            var testLatitude = 56.946M;
            var testLongitude = 24.1059M;

            var openMeteoTest = new OpenMeteoData();

            var apiData = OpenMeteoData(openMeteoTest);

            _weatherApi
                .Setup(api => api.GetApiWeather(testLatitude, testLongitude))
                .ThrowsAsync(new Exception());
            _validator
                .Setup(e => e.IsValidWeatherData(apiData))
                .Returns(false);

            _openMeteoService = new OpenMeteoService(_weatherApi.Object, new[] { _validator.Object });

            var result = await _openMeteoService.GetWeatherFromCoordinates(testLatitude, testLongitude);

            result.Should().BeNull();
        }

        private ApiResponse<OpenMeteoData> OpenMeteoData(OpenMeteoData openMeteoInfo)
        {
            var refitSettings = new RefitSettings();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            return new ApiResponse<OpenMeteoData>(responseMessage, openMeteoInfo, refitSettings);
        }
    }
}
