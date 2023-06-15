using WeatherByIp.Core.Models;

namespace WeatherByIp.Online.LocationDataAPI
{
    public interface IIpInfoService
    {
        Task<Location> GetMyLocation(string ip);
    }
}