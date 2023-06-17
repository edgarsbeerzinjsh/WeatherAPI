using FluentAssertions;
using Moq;
using System.Net;
using Refit;
using WeatherByIp.Core.Models;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Services;

namespace WeatherByIp.Tests
{
    public class IpInfoServiceTests
    {
        private IpInfoService _ipInfoService;
        private Mock<ILocationAPI> _locationApi;
        private Mock<IIpInfoDataValidation> _validator;

        [SetUp]
        public void Setup()
        {
            _locationApi = new Mock<ILocationAPI>();
            _validator = new Mock<IIpInfoDataValidation>();
        }

        [Test]
        public async Task GetMyLocation_ValidIp_LocationReturned()
        {
            var ip = "196.196.53.51";
            var ipInfoDefault = new IpInfoData()
            {
                city = "Riga",
                country = "LV",
                loc = "56.946, 24.1059",
                ip = "196.196.53.51"
            };
            var apiData = ApiIpData(ipInfoDefault); 

            var defaultLocation = new Location()
            {
                City = "Riga",
                Country = "LV",
                Ip = "196.196.53.51",
                Latitude = 56.946M,
                Longitude = 24.1059M
            };

            _locationApi
                .Setup(api => api.GetApiLocation(ip))
                .ReturnsAsync(apiData);
            _validator
                .Setup(e => e.IsValidLocationData(apiData))
                .Returns(true);

            _ipInfoService = new IpInfoService(_locationApi.Object, new [] {_validator.Object});


            var result = await _ipInfoService.GetMyLocation(ip);

            result.Should().BeEquivalentTo(defaultLocation, options => options.Excluding(t => t.EntryDateTime));
        }

        [Test]
        public async Task GetMyLocation_InvalidValidation_NullReturned()
        {
            var ip = "196.196.53.51";
            var ipInfoDefault = new IpInfoData()
            {
                city = "Riga",
                country = "LV",
                loc = "error",
                ip = "196.196.53.51"
            };
            var apiData = ApiIpData(ipInfoDefault);
            
            _locationApi
                .Setup(api => api.GetApiLocation(ip))
                .ReturnsAsync(apiData);
            _validator
                .Setup(e => e.IsValidLocationData(apiData))
                .Returns(false);

            _ipInfoService = new IpInfoService(_locationApi.Object, new[] { _validator.Object });


            var result = await _ipInfoService.GetMyLocation(ip);

            result.Should().BeNull();
        }

        [Test]
        public async Task GetMyLocation_InvalidResponse_NullReturned()
        {
            var ip = "196.196.53.51";
            var ipInfoDefault = new IpInfoData();

            var apiData = ApiIpData(ipInfoDefault);

            _locationApi
                .Setup(api => api.GetApiLocation(ip))
                .ThrowsAsync(new Exception());
            _validator
                .Setup(e => e.IsValidLocationData(apiData))
                .Returns(false);

            _ipInfoService = new IpInfoService(_locationApi.Object, new[] { _validator.Object });


            var result = await _ipInfoService.GetMyLocation(ip);

            result.Should().BeNull();
        }

        private ApiResponse<IpInfoData> ApiIpData(IpInfoData ipInfo)
        {
            var refitSettings = new RefitSettings();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            return new ApiResponse<IpInfoData>(responseMessage, ipInfo, refitSettings);
        }
    }
}