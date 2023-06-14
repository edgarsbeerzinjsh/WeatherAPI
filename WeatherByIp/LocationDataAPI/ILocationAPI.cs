using Refit;

namespace WeatherByIp
{
    public interface ILocationAPI
    {
        [Get("/{ipAddress}/json")]
        Task<ApiResponse<ipInfoData>> GetLocation(string ipAddress);
    }
}
