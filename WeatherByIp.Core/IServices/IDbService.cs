using WeatherByIp.Core.Models;

namespace WeatherByIp.Core.IServices
{
    public interface IDbService<T> where T : Entity
    {
        T Create(T entity);
        void ClearAll();
    }
}
