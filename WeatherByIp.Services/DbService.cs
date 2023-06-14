using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherByIp.Core.IServices;
using WeatherByIp.Data;
using Microsoft.EntityFrameworkCore;
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
        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual List<T> GetFiltered(Func<T, bool> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }
        public void ClearAll()
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }
    }
}
