using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
