using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services;
using WeatherByIp.Services.Validations;
using WeatherByIp.Services.Validations.IpInfoDataValidations;
using WeatherByIp.Services.Validations.OpenMeteoDataValidations;

namespace WeatherByIp.Tests
{
    [TestFixture]
    public class DependencyUtilsTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void RegisterValidations_RegistersAllValidationServices()
        {
            _services.RegisterValidations();

            _services.Should().Contain(x => x.ServiceType == typeof(IValidationOfIpAddress) && x.ImplementationType == typeof(ValidationOfIpAddress));
            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoDataValidation) && x.ImplementationType == typeof(IpInfoDataIsSuccessStatusCode));
            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoDataValidation) && x.ImplementationType == typeof(IpInfoDataHasIp));
            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoDataValidation) && x.ImplementationType == typeof(IpInfoDataHasCity));
            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoDataValidation) && x.ImplementationType == typeof(IpInfoDataHasCountry));
            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoDataValidation) && x.ImplementationType == typeof(IpInfoDataHasCoordinates));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataIsSuccessStatus));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasLatitude));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasLongitude));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasTemperature));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasWindspeed));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasWinddirection));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoDataValidation) && x.ImplementationType == typeof(OpenMeteoDataHasWeathercode));
        }

        [Test]
        public void RegisterServices_RegistersAllServices()
        {
            _services.RegisterServices();

            _services.Should().Contain(x => x.ServiceType == typeof(IIpInfoService) && x.ImplementationType == typeof(IpInfoService));
            _services.Should().Contain(x => x.ServiceType == typeof(IOpenMeteoService) && x.ImplementationType == typeof(OpenMeteoService));
            _services.Should().Contain(x => x.ServiceType == typeof(IDbService<Location>) && x.ImplementationType == typeof(DbService<Location>));
            _services.Should().Contain(x => x.ServiceType == typeof(IDbService<Weather>) && x.ImplementationType == typeof(DbService<Weather>));
            _services.Should().Contain(x => x.ServiceType == typeof(ILocationService) && x.ImplementationType == typeof(LocationService));
            _services.Should().Contain(x => x.ServiceType == typeof(IApiReturnInfoService) && x.ImplementationType == typeof(ApiReturnInfoService));
        }
    }
}

