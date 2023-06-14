using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Core.IServices
{
    public interface IDbService<T> where T : Entity
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        List<T> GetFiltered(Func<T, bool> filter);
        void ClearAll();
    }
}
