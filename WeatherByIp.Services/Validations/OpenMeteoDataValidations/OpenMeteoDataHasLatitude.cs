using Refit;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services.Validations.OpenMeteoDataValidations
{
    public class OpenMeteoDataHasLatitude : IOpenMeteoDataValidation
    {
        public bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData)
        {
            if (openMeteoData.Content?.latitude == null)
            {
                return false;
            };

            return openMeteoData.Content?.latitude >= -90 && openMeteoData.Content?.latitude <= 90;
        }
    }
}
