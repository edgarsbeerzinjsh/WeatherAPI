using Refit;

namespace WeatherByIp.Online.LocationDataAPI
{
    public interface ILocationAPI
    {
        [Get("/{ipAddress}/json")]
        Task<ApiResponse<IpInfoData>> GetApiLocation(string ipAddress);
    }
}
