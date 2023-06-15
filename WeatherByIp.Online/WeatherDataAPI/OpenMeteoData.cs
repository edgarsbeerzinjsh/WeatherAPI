namespace WeatherByIp.Online.WeatherDataAPI
{
    public class OpenMeteoData
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal generationtime_ms { get; set; }
        public decimal utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public decimal elevation { get; set; }
        public WeatherParameters current_weather { get; set; }
    }
}
