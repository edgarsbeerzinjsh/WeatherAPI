namespace WeatherByIp.Core.Models
{
    public class Weather: Entity
    {
        public decimal Temperature { get; set; }
        public decimal Windspeed { get; set; }
        public decimal WindDirection { get; set; }
        public string WeatherState { get; set; }
    }
}
