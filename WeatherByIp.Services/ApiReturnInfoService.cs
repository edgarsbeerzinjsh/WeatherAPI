using Microsoft.Extensions.Caching.Memory;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services
{
    public class ApiReturnInfoService : DbService<Weather>, IApiReturnInfoService
    {
        protected readonly IMemoryCache _cache;
        protected readonly IOpenMeteoService _openMeteoService;
        protected readonly ILocationService _locationService;
        public ApiReturnInfoService(
            IWeatherByIpDbContext context, 
            IMemoryCache cache, 
            IOpenMeteoService openMeteoService,
            ILocationService locationService) : base(context)
        {
            _cache = cache;
            _openMeteoService = openMeteoService;
            _locationService = locationService;
        }

        public async Task<ApiReturnInfo> GetCurrentWeather(string ip)
        {
            if (_cache.TryGetValue($"Weather_{ip}", out ApiReturnInfo returnInfo))
            {
                return returnInfo;
            };

            var location = await _locationService.GetLocation(ip);
            if (location == null)
            {
                return null;
            };

            var onlineWeather = await _openMeteoService.GetWeatherFromCoordinates(location.Latitude, location.Longitude);
            if (onlineWeather != null)
            {
                Create(onlineWeather);
                return SaveNewApiReturnInfo(new ApiReturnInfo(location, onlineWeather));
            };

            return null;
        }

        private ApiReturnInfo SaveNewApiReturnInfo(ApiReturnInfo returnInfo)
        {
            _cache.Set($"Weather_{returnInfo.Ip}", returnInfo, new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return returnInfo;
        }
    }
}
