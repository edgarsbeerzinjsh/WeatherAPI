using Refit;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services.Validations.OpenMeteoDataValidations
{
    public class OpenMeteoDataHasWinddirection : IOpenMeteoDataValidation
    {
        public bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData)
        {
            return openMeteoData.Content?.current_weather?.winddirection != null;
        }
    }
}
