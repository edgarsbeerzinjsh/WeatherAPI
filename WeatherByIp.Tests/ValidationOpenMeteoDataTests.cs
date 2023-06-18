using FluentAssertions;
using Refit;
using System.Net;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services.Validations.OpenMeteoDataValidations;

namespace WeatherByIp.Tests
{
    public class ValidationOpenMeteoDataTests
    {
        private IOpenMeteoDataValidation _validationStatus;
        private RefitSettings _defaultRefitSettings = new RefitSettings();
        private HttpResponseMessage _defaultResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        private OpenMeteoData _defaulOpenMeteoData = new OpenMeteoData();
        private WeatherParameters _defaultWeatherParameters = new WeatherParameters();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsValidWeatherData_IsSuccessStatusCodeOK_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataIsSuccessStatus();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                _defaulOpenMeteoData,
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.RequestTimeout)]
        [TestCase(HttpStatusCode.NotFound)]
        public void IsValidWeatherData_IsSuccessStatusCodeOtherCodes_ReturnFalse(HttpStatusCode code)
        {
            _validationStatus = new OpenMeteoDataIsSuccessStatus();
            var response = new ApiResponse<OpenMeteoData>(
                new HttpResponseMessage(code),
                _defaulOpenMeteoData,
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasTemperatureOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasTemperature();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = new WeatherParameters() { temperature = 20 }},
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoTemperature_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasTemperature();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = { } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasWindspeedOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasWindspeed();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = new WeatherParameters() { windspeed = 20 } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoWindspeed_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasWindspeed();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = { } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasWinddirectionOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasWinddirection();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = new WeatherParameters() { winddirection = 20 } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoWinddirection_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasWinddirection();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = { } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasWeathercodeOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasWeathercode();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = new WeatherParameters() { weathercode = 20 } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoWeathercode_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasWeathercode();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { current_weather = { } },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasLatitudeOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasLatitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { latitude = 20 },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoLatitude_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasLatitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                null,
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        [TestCase(-200)]
        [TestCase(200)]
        public void IsValidWeatherData_HasLatitudeBad_ReturnFalse(decimal returnLatitude)
        {
            _validationStatus = new OpenMeteoDataHasLatitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { latitude = returnLatitude },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidWeatherData_HasLongitudeOk_ReturnTrue()
        {
            _validationStatus = new OpenMeteoDataHasLongitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { longitude = 20 },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeTrue();
        }

        [Test]
        public void IsValidWeatherData_HasNoLongitude_ReturnFalse()
        {
            _validationStatus = new OpenMeteoDataHasLongitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                null,
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }

        [Test]
        [TestCase(-200)]
        [TestCase(200)]
        public void IsValidWeatherData_HasLongitudeBad_ReturnFalse(decimal returnLongitude)
        {
            _validationStatus = new OpenMeteoDataHasLongitude();
            var response = new ApiResponse<OpenMeteoData>(
                _defaultResponseMessage,
                new OpenMeteoData() { longitude = returnLongitude },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidWeatherData(response);

            result.Should().BeFalse();
        }
    }
}
