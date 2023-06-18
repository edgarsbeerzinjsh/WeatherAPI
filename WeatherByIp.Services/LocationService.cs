using Microsoft.Extensions.Caching.Memory;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services
{
    public class LocationService : DbService<Location>, ILocationService
    {
        protected readonly IMemoryCache _cache;
        protected readonly IIpInfoService _ipInfoService;
        public LocationService(IWeatherByIpDbContext context, IMemoryCache cache, IIpInfoService ipInfoService) : base(context)
        {
            _cache = cache;
            _ipInfoService = ipInfoService;
        }

        public async Task<Location> GetLocation(string ip)
        {
            if (_cache.TryGetValue($"Location_{ip}", out Location cachedLocation))
            {
                return cachedLocation;
            };

            var dbData = _context.Locations.FirstOrDefault(l => l.Ip == ip);
            if (dbData != null)
            {
                return SaveNewLocation(dbData);
            };

            var onlineData = await _ipInfoService.GetMyLocation(ip);
            if (onlineData != null)
            {
                Create(onlineData);
                return SaveNewLocation(onlineData);
            };

            return null;
        }

        private Location SaveNewLocation(Location location)
        {
            _cache.Set($"Location_{location.Ip}", location, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return location;
        }
    }
}
