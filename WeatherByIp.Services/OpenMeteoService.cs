using WeatherByIp.Core.Models;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Online.WeatherDataAPI;

namespace WeatherByIp.Services
{
    public class OpenMeteoService : IOpenMeteoService
    {
        private readonly IWeatherAPI _weatherApi;
        private readonly IEnumerable<IOpenMeteoDataValidation> _validators;
        public OpenMeteoService(IWeatherAPI weatherApi, IEnumerable<IOpenMeteoDataValidation> validators)
        {
            _weatherApi = weatherApi;
            _validators = validators;
        }

        public async Task<Weather> GetWeatherFromCoordinates(decimal latitude, decimal longitude)
        {
            try
            {
                var weather = await _weatherApi.GetApiWeather(latitude, longitude);
                
                if (!_validators.All(v => v.IsValidWeatherData(weather)))
                {
                    return null;
                }

                return new Weather()
                {
                    Latitude = weather.Content.latitude,
                    Longitude = weather.Content.longitude,
                    Temperature = weather.Content.current_weather.temperature,
                    Windspeed = weather.Content.current_weather.windspeed,
                    WindDirection = weather.Content.current_weather.winddirection,
                    WeatherState = GetWeatherDescription(weather.Content.current_weather.weathercode)
                };
            }
            catch
            {
                return null;
            }
        }

        private string GetWeatherDescription(int code)
        {
            var codeDescription = new Dictionary<int, string>()
            {
                { 0, "Clear sky" },
                { 1, "Mainly clear" },
                { 2, "Partly cloudy" },
                { 3, "Overcast" },
                { 45, "Fog and depositing rime fog" },
                { 48, "Fog and depositing rime fog" },
                { 51, "Drizzle: Light intensity" },
                { 53, "Drizzle: Moderate intensity" },
                { 55, "Drizzle: Dense intensity" },
                { 56, "Freezing Drizzle: Light intensity" },
                { 57, "Freezing Drizzle: Dense intensity" },
                { 61, "Rain: Slight intensity" },
                { 63, "Rain: Moderate intensity" },
                { 65, "Rain: Heavy intensity" },
                { 66, "Freezing Rain: Light intensity" },
                { 67, "Freezing Rain: Heavy intensity" },
                { 71, "Snow fall: Slight intensity" },
                { 73, "Snow fall: Moderate intensity" },
                { 75, "Snow fall: Heavy intensity" },
                { 77, "Snow grains" },
                { 80, "Rain showers: Slight intensity" },
                { 81, "Rain showers: Moderate intensity" },
                { 82, "Rain showers: Violent intensity" },
                { 85, "Snow showers: Slight intensity" },
                { 86, "Snow showers: Heavy intensity" }
            };

            return codeDescription.TryGetValue(code, out string value) ? value : "Unknown weather code";
        }
    }
}
