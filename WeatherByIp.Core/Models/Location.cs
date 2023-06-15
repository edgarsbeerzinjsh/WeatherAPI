namespace WeatherByIp.Core.Models
{
    public class Location: Entity
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
