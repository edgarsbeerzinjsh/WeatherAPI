using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Services;
using MockQueryable.Moq;

namespace WeatherByIp.Tests
{
    public class LocationServiceTests
    {
        private LocationService _locationService;
        private MemoryCache _cache;
        private Mock<IIpInfoService> _ipInfoService;
        private WeatherByIpDbContext _context;
        private DbService<Location> _dbLocation;

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _ipInfoService = new Mock<IIpInfoService>();
            SetUpCache();
            SetUpDb();
            SetUpApi();
            _locationService = new LocationService(_context, _cache, _ipInfoService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetLocation_cacheIp_LocationFromCacheReturned()
        {
            var ip = cacheLocation.Ip;

            var result = await _locationService.GetLocation(ip);

            result.Should().BeEquivalentTo(cacheLocation);
        }

        [Test]
        public async Task GetLocation_dbIp_LocationFromDbReturned()
        {
            var ip = dbLocation.Ip;

            var result = await _locationService.GetLocation(ip);

            result.Should().BeEquivalentTo(dbLocation);
        }

        [Test]
        public async Task GetLocation_apiIp_LocationFromApiReturned()
        {
            var ip = apiLocation.Ip;

            var result = await _locationService.GetLocation(ip);

            result.Should().BeEquivalentTo(apiLocation);
        }

        [Test]
        public async Task GetLocation_randomIp_NullReturned()
        {
            var ip = "1.1.1.1";

            var result = await _locationService.GetLocation(ip);

            result.Should().BeNull();
        }

        private void SetUpCache()
        {
            _cache.Set($"Location_{cacheLocation.Ip}", cacheLocation, TimeSpan.FromMinutes(2));
        }

        private void SetUpDb()
        {
            var options = new DbContextOptionsBuilder<WeatherByIpDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WeatherByIpDbContext(options);
            _context.Database.EnsureCreated();
            _dbLocation = new DbService<Location>(_context);
            _dbLocation.Create(dbLocation);
        }

        private void SetUpApi()
        {
            _ipInfoService
                .Setup(a => a.GetMyLocation(apiLocation.Ip))
                .ReturnsAsync(apiLocation);
        }

        private Location cacheLocation = new Location
        {
            Id = 111,
            City = "Riga",
            Country = "LV",
            Ip = "196.196.53.51",
            Latitude = 56.946M,
            Longitude = 24.1059M,
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
    }
}
