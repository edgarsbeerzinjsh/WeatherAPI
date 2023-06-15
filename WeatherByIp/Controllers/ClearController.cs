using Microsoft.AspNetCore.Mvc;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClearController : ControllerBase
    {
        private readonly IDbService<Location> _locationService;
        private readonly IDbService<Weather> _weatherService;
        public ClearController(IDbService<Location> locationService, IDbService<Weather> weatherService)
        {
            _locationService = locationService;
            _weatherService = weatherService;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDb()
        {
            _locationService.ClearAll();
            _weatherService.ClearAll();

            return Ok("Db cleared");
        }
    }
}
