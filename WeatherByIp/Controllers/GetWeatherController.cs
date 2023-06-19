using Microsoft.AspNetCore.Mvc;
using WeatherByIp.Core.IServices;

namespace WeatherByIp.Controllers
{
    [Route("weather")]
    [ApiController]
    public class GetWeatherController : ControllerBase
    {
        private readonly IApiReturnInfoService _apiReturnInfoService;

        public GetWeatherController(IApiReturnInfoService apiReturnInfoService)
        {
            _apiReturnInfoService = apiReturnInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            //var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var ipAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault();

            var returnInfo = await _apiReturnInfoService.GetCurrentWeather(ipAddress.ToString());
            if (returnInfo != null)
            {
                return Ok(returnInfo);
            };

            return NotFound("Did not get current weather information from network");
        }
    }
}
