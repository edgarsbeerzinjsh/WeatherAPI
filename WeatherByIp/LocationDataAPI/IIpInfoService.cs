using WeatherByIp.Core.Models;

namespace WeatherByIp.LocationDataAPI
{
    public interface IIpInfoService
    {
        Task<Location> GetLocation(string ip);
    }
}