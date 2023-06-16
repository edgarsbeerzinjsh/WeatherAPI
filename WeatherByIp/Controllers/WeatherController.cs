using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherByIp.Core.IServices;
using WeatherByIp.Services.Validations;

namespace WeatherByIp.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IApiReturnInfoService _apiReturnInfoService;
        private readonly IValidationOfIpAddress _validationOfIpAddress;

        public WeatherController(IApiReturnInfoService apiReturnInfoService, IValidationOfIpAddress validationOfIpAddress)
        {
            _apiReturnInfoService = apiReturnInfoService;
            _validationOfIpAddress = validationOfIpAddress;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

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
            var validIp = _validationOfIpAddress.IsValidIpAddress(ipAddress);
            if (validIp == null)
            {
                return BadRequest("Not valid IP address provided");
            }

            var returnInfo = await _apiReturnInfoService.GetCurrentWeather(validIp.ToString());
            if (returnInfo != null)
            {
                return Ok(returnInfo);
            }



            return NotFound("Did not get current weather information from network");
        }
    }
}