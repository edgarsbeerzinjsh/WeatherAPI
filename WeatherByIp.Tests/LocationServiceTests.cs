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
        private Mock<IWeatherByIpDbContext> _context;

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _ipInfoService = new Mock<IIpInfoService>();
            _context = new Mock<IWeatherByIpDbContext>();
        }

        [Test]
        public async Task GetLocation_cacheIp_LocationFromCacheReturned()
        {
            var ip = cacheLocation.Ip;
            _cache.Set($"Location_{ip}", cacheLocation, TimeSpan.FromMinutes(2));

            //_context
            //    .Setup(d => d.Locations.FirstOrDefault(l => l.Ip == ip))
            //    .Returns(dbLocation);
            _ipInfoService
                .Setup(a => a.GetMyLocation(ip))
                .ReturnsAsync(apiLocation);

            _locationService = new LocationService(_context.Object, _cache, _ipInfoService.Object);

            var result = await _locationService.GetLocation(ip);

            result.Should().BeEquivalentTo(cacheLocation);
        }

        //[Test]
        //public async Task GetLocation_dbIp_LocationFromDbReturned()
        //{
        //    var ip = cacheLocation.Ip;
        //    _cache.Set($"Location_{ip}", cacheLocation, TimeSpan.FromMinutes(2));

        //    //_context
        //    //    .Setup(d => d.Locations.FirstOrDefault(l => l.Ip == ip))
        //    //    .Returns(dbLocation);
        //    _ipInfoService
        //        .Setup(a => a.GetMyLocation(ip))
        //        .ReturnsAsync(apiLocation);

        //    _locationService = new LocationService(_context.Object, _cache, _ipInfoService.Object);

        //    var result = await _locationService.GetLocation(ip);

        //    result.Should().BeEquivalentTo(cacheLocation);
        //}

        [Test]
        public async Task GetLocation_apiIp_LocationFromApiReturned()
        {
            var ip = apiLocation.Ip;
            //_cache.Set($"Location_{ip}", cacheLocation, TimeSpan.FromMinutes(2));

            //_context
            //    .Setup(d => d.Locations.FirstOrDefault(l => l.Ip == ip))
            //    .Returns(dbLocation);
            //_ipInfoService
            //    .Setup(a => a.GetMyLocation(ip))
            //    .ReturnsAsync(apiLocation);

            _locationService = new LocationService(_context.Object, _cache, _ipInfoService.Object);

            var result = await _locationService.GetLocation(ip);

            result.Should().BeEquivalentTo(apiLocation);
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

        private List<Location> dbSetLocations = new List<Location>()
        {
            new Location
            {
                Id = 112,
                City = "Paris",
                Country = "FR",
                Ip = "13.36.82.241",
                Latitude = 48.8323M,
                Longitude = 2.4075M,
                EntryDateTime = DateTime.Now
            }
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
