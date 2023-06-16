﻿using Refit;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services.Validations.OpenMeteoDataValidations
{
    public class OpenMeteoDataHasWindspeed : IOpenMeteoDataValidation
    {
        public bool IsValidWeatherData(ApiResponse<OpenMeteoData> openMeteoData)
        {
            return openMeteoData.Content?.current_weather?.windspeed != null;
        }
    }
}
