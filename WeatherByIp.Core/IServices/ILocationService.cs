using WeatherByIp.Core.Models;

namespace WeatherByIp.Core.IServices
{
    public interface ILocationService
    {
        Task<Location> GetLocation(string ip);
    }
}
