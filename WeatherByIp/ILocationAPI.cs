using Refit;

namespace WeatherByIp
{
    public interface ILocationAPI
    {
        [Get("/{ipAddress}/json")]
        Task<ipInfoData> GetLocation(string ipAddress);
    }
}
