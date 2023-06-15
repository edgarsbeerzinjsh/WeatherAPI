using Microsoft.EntityFrameworkCore;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Data
{
    public class WeatherByIpDbContext: DbContext, IWeatherByIpDbContext
    {
        public WeatherByIpDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> WeatherData { get; set; }
    }
}
