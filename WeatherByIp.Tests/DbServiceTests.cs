using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Services;

namespace WeatherByIp.Tests
{
    public class DbServiceTests
    {
        private WeatherByIpDbContext _context;
        private DbService<Location> _dbLocation;
        private DbService<Weather> _dbWeather;

        [SetUp]
        public void Setup()
        {
            TestDbSetup();
            _dbLocation = new DbService<Location>(_context);
            _dbWeather = new DbService<Weather>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void CreateLocation_ValidInfo_LocationsInDatabaseOneMore()
        {
            var alreadyLocation = _context.Locations.Count();

            _dbLocation.Create(defaultLocation);

            _context.Locations.Count().Equals(alreadyLocation + 1);
        }

        [Test]
        public void CreateLocation_ValidInfo_SameLocationReturned()
        {
            var newLocation = _dbLocation.Create(defaultLocation);

            newLocation.Should().Be(defaultLocation);
        }

        [Test]
        public void DeleteAllLocations_DeleteLocations_NoLocationsInDb()
        {
            var alreadyLocations = _context.Locations.Count();

            _dbLocation.ClearAll();

            alreadyLocations.Should().BeGreaterThan(0);
            _context.Locations.Count().Equals(0);
        }

        [Test]
        public void CreateWeather_ValidInfo_WeathersInDatabaseOneMore()
        {
            var alreadyWeather = _context.WeatherData.Count();

            _dbWeather.Create(defaultWeather);

            _context.WeatherData.Count().Equals(alreadyWeather + 1);
        }

        [Test]
        public void CreateWeather_ValidInfo_SameWeatherReturned()
        {
            var newWeather = _dbWeather.Create(defaultWeather);

            newWeather.Should().Be(defaultWeather);
        }

        [Test]
        public void DeleteAllWeathers_DeleteWeather_NoWeatherInDb()
        {
            var alreadyWeather = _context.WeatherData.Count();

            _dbWeather.ClearAll();

            alreadyWeather.Should().BeGreaterThan(0);
            _context.WeatherData.Count().Equals(0);
        }

        private void TestDbSetup()
        {
            var options = new DbContextOptionsBuilder<WeatherByIpDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WeatherByIpDbContext(options);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Database.EnsureCreated();

            _context.Locations.Add(new Location
            {
                Id = 11,
                City = "Riga",
                Country = "LV",
                Ip = "196.196.53.51",
                Latitude = 56.946M,
                Longitude = 24.1059M,
                EntryDateTime = DateTime.Now
            });

            _context.SaveChanges();

            _context.WeatherData.Add(new Weather
            {
                Latitude = 56.946M,
                Longitude = 24.1059M,
                Temperature = 19,
                Windspeed = 20,
                WindDirection = 21,
                WeatherState = "Mainly clear",
                Id = 1,
                EntryDateTime = DateTime.Now
            });

            _context.SaveChanges();
        }

        private Location defaultLocation = new Location
        {
            Id = 111,
            City = "Riga",
            Country = "LV",
            Ip = "196.196.53.51",
            Latitude = 56.946M,
            Longitude = 24.1059M,
            EntryDateTime = DateTime.Now
        };

        private Weather defaultWeather = new Weather
        {
            Latitude = 56.946M,
            Longitude = 24.1059M,
            Temperature = 39,
            Windspeed = 10,
            WindDirection = 1,
            WeatherState = "Mainly clear",
            Id = 23,
            EntryDateTime = DateTime.Now
        };
    }
}
