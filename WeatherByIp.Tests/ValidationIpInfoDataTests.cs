using System.Net;
using FluentAssertions;
using Refit;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Services.Validations.IpInfoDataValidations;

namespace WeatherByIp.Tests
{
    public class ValidationIpInfoDataTests
    {
        //private Mock<ApiResponse<IpInfoData>> _apiResponse;
        private IIpInfoDataValidation _validationStatus;
        private RefitSettings _defaultRefitSettings = new RefitSettings();
        private HttpResponseMessage _defaultResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        private IpInfoData _defaultIpInfoData = new IpInfoData();

        [SetUp]
        public void Setup()
        {
            //_validationStatus = new IpInfoDataIsSuccessStatusCode();
            //_apiResponse = new Mock<ApiResponse<IpInfoData>>();
        }

        [Test]
        public void IsValidLocationData_IsSuccessStatusCodeOK_ReturnTrue()
        {
            _validationStatus = new IpInfoDataIsSuccessStatusCode();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                _defaultIpInfoData,
                _defaultRefitSettings
            );
            //_apiResponse
            //    .Setup(api => api.IsSuccessStatusCode)
            //    .Returns(true);
            //var response = _apiResponse.Object;

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.RequestTimeout)]
        [TestCase(HttpStatusCode.NotFound)]
        public void IsValidLocationData_IsSuccessStatusCodeOtherCodes_ReturnFalse(HttpStatusCode code)
        {
            _validationStatus = new IpInfoDataIsSuccessStatusCode();
            var response = new ApiResponse<IpInfoData>(
                new HttpResponseMessage(code),
                _defaultIpInfoData,
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidLocationData_HasIpOk_ReturnTrue()
        {
            _validationStatus = new IpInfoDataHasIp();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() {ip = "11.11.11.11"},
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsValidLocationData_HasIpBad_ReturnFalse(string returnIp)
        {
            _validationStatus = new IpInfoDataHasIp();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { ip = returnIp },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidLocationData_HasCityOk_ReturnTrue()
        {
            _validationStatus = new IpInfoDataHasCity();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { city = "Riga" },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsValidLocationData_HasCityBad_ReturnFalse(string returnCity)
        {
            _validationStatus = new IpInfoDataHasCity();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { city = returnCity },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidLocationData_HasCountryOk_ReturnTrue()
        {
            _validationStatus = new IpInfoDataHasCountry();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { country = "LV" },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void IsValidLocationData_HasCountryBad_ReturnFalse(string returnCountry)
        {
            _validationStatus = new IpInfoDataHasCountry();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { country = returnCountry },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeFalse();
        }

        [Test]
        public void IsValidLocationData_HasCoordinatesOk_ReturnTrue()
        {
            _validationStatus = new IpInfoDataHasCoordinates();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { loc = "0, 0" },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeTrue();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a, a")]
        [TestCase("0, 0, 0")]
        [TestCase("0")]
        [TestCase("0. 0")]
        [TestCase("-200, 0")]
        [TestCase("200, 0")]
        [TestCase("0, -200")]
        [TestCase("0, 200")]
        [TestCase("200, 200")]
        public void IsValidLocationData_HasCoordinatesBad_ReturnFalse(string returnCoordinates)
        {
            _validationStatus = new IpInfoDataHasCoordinates();
            var response = new ApiResponse<IpInfoData>(
                _defaultResponseMessage,
                new IpInfoData() { loc = returnCoordinates },
                _defaultRefitSettings
            );

            var result = _validationStatus.IsValidLocationData(response);

            result.Should().BeFalse();
        }
    }
}
