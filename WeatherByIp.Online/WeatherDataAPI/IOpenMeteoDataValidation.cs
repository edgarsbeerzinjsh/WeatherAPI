using Refit;

namespace WeatherByIp.Online.WeatherDataAPI
{
    public interface IOpenMeteoDataValidation
    {
        bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData);
    }
}
