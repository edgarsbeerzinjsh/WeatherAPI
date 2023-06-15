using Refit;

namespace WeatherByIp.Online.WeatherDataAPI
{
    public interface IWeatherAPI
    {
        [Get("/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true")]
        Task<ApiResponse<OpenMeteoData>> GetApiWeather(decimal latitude, decimal longitude);
    }
}
