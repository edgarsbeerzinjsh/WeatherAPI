using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Refit;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.LocationDataAPI;
using WeatherByIp.Models;

namespace WeatherByIp.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IIpInfoService _ipInfoService;
        private readonly IWeatherAPI _weatherApi;
        private readonly IDbService<Location> _ipLocation;
        private readonly IDbService<Weather> _locWeather;
        private readonly IMemoryCache _cache;
        public WeatherController(
            IIpInfoService ipInfoService,
            IWeatherAPI weather,
            IDbService<Location> ipLocation,
            IDbService<Weather> locWeather,
            IMemoryCache cache)
        {
            _ipInfoService = ipInfoService;
            _weatherApi = weather;
            _ipLocation = ipLocation;
            _locWeather = locWeather;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string ipstring)
        {
            //string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var ipAddress = ipstring;

            if (_cache.TryGetValue($"Weather_{ipAddress}", out ApiReturnInfo returnInfo))
            {
                return Ok(returnInfo);
            };

            if (_cache.TryGetValue($"Location_{ipAddress}", out Location cachedLocation))
            {
                return await GetWeatherData(cachedLocation);
            };

            try
            {
                var newLocation = await _ipInfoService.GetLocation(ipAddress);
                if (newLocation != null)
                {
                    _ipLocation.Create(newLocation);

                    _cache.Set($"Location_{ipAddress}", newLocation, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

                    return await GetWeatherData(newLocation);
                }
            }
            catch (Exception ex)
            {
                var location = _ipLocation.GetFiltered(e => e.Ip == ipAddress).OrderBy(e => e.EntryDateTime);
                return BadRequest(location);
            }

            return NotFound();
        }

        private async Task<IActionResult> GetWeatherData(Location location)
        {
            if (_cache.TryGetValue($"Weather_{location.Ip}", out ApiReturnInfo cachedWeather))
            {
                return Ok(cachedWeather);
            };

            var weather = await _weatherApi.GetCurrentWeather(location.Latitude, location.Longitude);

            if (weather != null)

            {
                var currentWeather = new Weather()
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Temperature = weather.Content.current_weather.temperature,
                    Windspeed = weather.Content.current_weather.windspeed,
                    WindDirection = weather.Content.current_weather.winddirection,
                    WeatherState = WeatherParameters.GetWeatherDescription(weather.Content.current_weather.weathercode)
                };

                _locWeather.Create(currentWeather);

                var returnInfo = new ApiReturnInfo(location, currentWeather);

                _cache.Set($"Weather_{location.Ip}", returnInfo, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

                return Ok(returnInfo);
            }

            return NotFound();
        }

        //[HttpGet]
        //[Route("Ip")]
        //public async Task<IActionResult> CheckWeather(string ipstring)
        //{
        //    var ipAddress = ipstring;

        //    var location = await _locationApi.GetLocation(ipAddress);

        //    Decimal.TryParse(location.Content.loc.Split(',')[0], out var latitude);
        //    Decimal.TryParse(location.Content.loc.Split(',')[1], out var longitude);

        //    if (location != null)
        //    {
        //        var weather = await _weatherApi.GetCurrentWeather(latitude, longitude);

        //        if (weather != null)

        //        {
        //            return Ok($"Temperature: {weather.Content.current_weather.temperature}\n " +
        //                $"Windspeed: {weather.Content.current_weather.windspeed}");
        //        }
        //    }

        //    return NotFound();
        //}

        [HttpDelete]
        [Route("DeleteDb")]
        public async Task<IActionResult> DeleteDb()
        {
            _ipLocation.ClearAll();
            _locWeather.ClearAll();

            return Ok("Db cleared");
        }
    }
}