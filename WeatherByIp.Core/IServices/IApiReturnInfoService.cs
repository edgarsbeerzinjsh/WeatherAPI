using WeatherByIp.Core.Models;

namespace WeatherByIp.Core.IServices
{
    public interface IApiReturnInfoService
    {
        Task<ApiReturnInfo> GetCurrentWeather(string ip);
    }
}
