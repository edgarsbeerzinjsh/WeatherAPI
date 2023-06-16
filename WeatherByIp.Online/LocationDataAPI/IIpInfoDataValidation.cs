using Refit;

namespace WeatherByIp.Online.LocationDataAPI
{
    public interface IIpInfoDataValidation
    {
        bool IsValidLocationData(ApiResponse<IpInfoData> ipInfoData);
    }
}
