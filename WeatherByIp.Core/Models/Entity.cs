namespace WeatherByIp.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime EntryDateTime { get; set; } = DateTime.UtcNow;
    }
}
