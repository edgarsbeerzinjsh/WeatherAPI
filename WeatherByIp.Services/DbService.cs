using WeatherByIp.Core.IServices;
using WeatherByIp.Data;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Services
{
    public class DbService<T> : IDbService<T> where T : Entity
    {
        protected readonly IWeatherByIpDbContext _context;
        public DbService(IWeatherByIpDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }
        public void ClearAll()
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
