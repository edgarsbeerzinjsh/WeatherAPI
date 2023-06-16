using Refit;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services.Validations.IpInfoDataValidations
{
    public class IpInfoDataIsSuccessStatusCode : IIpInfoDataValidation
    {
        public bool IsValidLocationData(ApiResponse<IpInfoData> ipInfoData)
        {
            return ipInfoData.IsSuccessStatusCode;
        }
    }
}
