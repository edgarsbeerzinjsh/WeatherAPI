using Microsoft.AspNetCore.Mvc;
using WeatherByIp.Core.IServices;


namespace WeatherByIp.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IApiReturnInfoService _apiReturnInfoService;

        public WeatherController(IApiReturnInfoService apiReturnInfoService)
        {
            _apiReturnInfoService = apiReturnInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string ipstring)
        {
            //string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var ipAddress = ipstring;

            var returnInfo = await _apiReturnInfoService.GetCurrentWeather(ipAddress);
            if (returnInfo != null)
            {
                return Ok(returnInfo);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("Ip")]
        public async Task<IActionResult> CheckWeather(string ipAddress)
        {
            var returnInfo = await _apiReturnInfoService.GetCurrentWeather(ipAddress);
            if (returnInfo != null)
            {
                return Ok(returnInfo);
            }

            return NotFound();
        }
    }
}