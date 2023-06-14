using WeatherByIp.Core.Models;

namespace WeatherByIp.Models
{
    public class ApiReturnInfo
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Temperature { get; set; }
        public decimal Windspeed { get; set; }
        public decimal WindDirection { get; set; }
        public string WeatherState { get; set; }

        public ApiReturnInfo(Location location, Weather weather)
        {
            Ip = location.Ip;
            City = location.City;
            Country = location.Country;
            Temperature = weather.Temperature;
            Windspeed = weather.Windspeed;
            WindDirection = weather.WindDirection;
            WeatherState = weather.WeatherState;
        }
    }
}
