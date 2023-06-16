using Refit;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services.Validations.OpenMeteoDataValidations
{
    public class OpenMeteoDataIsSuccessStatus : IOpenMeteoDataValidation
    {
        public bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData)
        {
            return openMeteoData.IsSuccessStatusCode;
        }
    }
}
