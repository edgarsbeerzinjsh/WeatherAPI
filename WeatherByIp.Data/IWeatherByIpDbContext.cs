using Microsoft.EntityFrameworkCore;
using WeatherByIp.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WeatherByIp.Data
{
    public interface IWeatherByIpDbContext
    {
        DbSet<Location> Locations { get; set; }
        DbSet<Weather> WeatherData { get; set; }

        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
