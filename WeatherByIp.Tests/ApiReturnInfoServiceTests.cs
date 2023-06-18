using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services;

namespace WeatherByIp.Tests
{
    public class ApiReturnInfoServiceTests
    {
        private ApiReturnInfoService _returnInfoService;
        private MemoryCache _cache;
        private Mock<IOpenMeteoService> _openMeteoService;
        private Mock<ILocationService> _locationService;
        private WeatherByIpDbContext _context;
        private DbService<Weather> _dbWeather;

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _openMeteoService = new Mock<IOpenMeteoService>();
            _locationService = new Mock<ILocationService>();
            SetUpCache();
            SetUpDbLocationMock();
            SetUpDb();
            SetUpApi();
            _returnInfoService = new ApiReturnInfoService(_context, _cache, _openMeteoService.Object, _locationService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetCurrentWeather_cacheIp_ReturnInfoFromCacheReturned()
        {
            var ip = cacheReturnInfo.Ip;

            var result = await _returnInfoService.GetCurrentWeather(ip);

            result.Should().BeEquivalentTo(cacheReturnInfo);
        }

        [Test]
        public async Task GetCurrentWeather_dbIp_ReturnInfoFromDbReturned()
        {
            var ip = dbLocation.Ip;

            var result = await _returnInfoService.GetCurrentWeather(ip);

            result.Should().BeEquivalentTo(new ApiReturnInfo(dbLocation, dbWeather));
        }

        [Test]
        public async Task GetCurrentWeather_apiIp_ReturnInfoFromApiReturned()
        {
            var ip = apiLocation.Ip;

            var result = await _returnInfoService.GetCurrentWeather(ip);

            result.Should().BeEquivalentTo(new ApiReturnInfo(apiLocation, apiWeather));
        }

        [Test]
        public async Task GetCurrentWeather_onlyLocationDbIp_NullReturned()
        {
            var ip = dbOnlyLocation.Ip;

            var result = await _returnInfoService.GetCurrentWeather(ip);

            result.Should().BeNull();
        }

        [Test]
        public async Task GetCurrentWeather_randomIp_NullReturned()
        {
            var ip = "1.1.1.1";

            var result = await _returnInfoService.GetCurrentWeather(ip);

            result.Should().BeNull();
        }

        private void SetUpCache()
        {
            _cache.Set($"Weather_{cacheReturnInfo.Ip}", cacheReturnInfo, TimeSpan.FromMinutes(2));
        }

        private void SetUpDb()
        {
            var options = new DbContextOptionsBuilder<WeatherByIpDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WeatherByIpDbContext(options);
            _context.Database.EnsureCreated();
            _dbWeather = new DbService<Weather>(_context);
        }

        private void SetUpDbLocationMock()
        {
            _locationService
                .Setup(l => l.GetLocation(apiLocation.Ip))
                .ReturnsAsync(apiLocation);

            _locationService
                .Setup(l => l.GetLocation(dbOnlyLocation.Ip))
                .ReturnsAsync(dbOnlyLocation);

            _locationService
                .Setup(l => l.GetLocation(dbLocation.Ip))
                .ReturnsAsync(dbLocation);
        }

        private void SetUpApi()
        {
            _openMeteoService
                .Setup(a => a.GetWeatherFromCoordinates(apiLocation.Latitude, apiLocation.Longitude))
                .ReturnsAsync(apiWeather);

            _openMeteoService
                .Setup(a => a.GetWeatherFromCoordinates(dbLocation.Latitude, dbLocation.Longitude))
                .ReturnsAsync(dbWeather);
        }

        private ApiReturnInfo cacheReturnInfo = new ApiReturnInfo
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

        private Location dbOnlyLocation = new Location
        {
            Id = 115,
            City = "London",
            Country = "GB",
            Ip = "185.182.71.38",
            Latitude = 51.5638M,
            Longitude = -0.0765M,
            EntryDateTime = DateTime.Now
        };

        private Location dbLocation = new Location
        {
            Id = 112,
            City = "Paris",
            Country = "FR",
            Ip = "13.36.82.241",
            Latitude = 48.8323M,
            Longitude = 2.4075M,
            EntryDateTime = DateTime.Now
        };

        private Weather dbWeather = new Weather
        {
            Latitude = 48.8323M,
            Longitude = 2.4075M,
            Temperature = 29,
            Windspeed = 30,
            WindDirection = 31,
            WeatherState = "Snow grains"
        };

        private Location apiLocation = new Location
        {
            Id = 113,
            City = "Tallinn",
            Country = "EE",
            Ip = "194.126.96.4",
            Latitude = 59.4381M,
            Longitude = 24.7369M,
            EntryDateTime = DateTime.Now
        };

        private Weather apiWeather = new Weather
        {
            Latitude = 59.4381M,
            Longitude = 24.7369M,
            Temperature = 39,
            Windspeed = 40,
            WindDirection = 41,
            WeatherState = "Overcast"
        };
    }
}
