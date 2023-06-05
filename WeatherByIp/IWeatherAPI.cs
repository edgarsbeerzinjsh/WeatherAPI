using Refit;

namespace WeatherByIp
{
    public interface IWeatherAPI
    {
        [Get("/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true")]
        Task<openMeteoData> GetCurrentWeather(decimal latitude, decimal longitude);
    }
}
