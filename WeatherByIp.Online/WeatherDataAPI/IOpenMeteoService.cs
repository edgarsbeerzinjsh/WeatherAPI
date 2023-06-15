using WeatherByIp.Core.Models;

namespace WeatherByIp.Online.WeatherDataAPI
{
    public interface IOpenMeteoService
    {
        Task<Weather> GetWeatherFromCoordinates(decimal latitude, decimal longitude);
    }
}