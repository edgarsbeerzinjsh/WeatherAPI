using Refit;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services.Validations.OpenMeteoDataValidations
{
    public class OpenMeteoDataHasLongitude : IOpenMeteoDataValidation
    {
        public bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData)
        {
            if (openMeteoData.Content?.latitude == null)
            {
                return false;
            }

            return openMeteoData.Content?.longitude >= -180 && openMeteoData.Content?.longitude <= 180;
        }
    }
}
