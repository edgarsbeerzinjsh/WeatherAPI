using Microsoft.AspNetCore.Mvc;
using Refit;

namespace WeatherByIp.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public async Task <IActionResult> GetWeather(string ipstring)
        {
            //string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var ipAddress = ipstring;

            var locationService = RestService.For<ILocationAPI>("https://ipinfo.io");
            var location = await locationService.GetLocation(ipAddress);
            
            Decimal.TryParse(location.loc.Split(',')[0], out var latitude);
            Decimal.TryParse(location.loc.Split(',')[1], out var longitude);

            if (location != null)
            {
                var weatherService = RestService.For<IWeatherAPI>("https://api.open-meteo.com");
                var weather = await weatherService.GetCurrentWeather(latitude, longitude);

                if (weather != null)

                {
                    return Ok($"Temperature: {weather.current_weather.temperature}\n Windspeed: {weather.current_weather.windspeed}");
                }
            }

            return NotFound();
        }
    }
}