using Refit;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services.Validations.IpInfoDataValidations
{
    public class IpInfoDataHasCity : IIpInfoDataValidation
    {
        public bool IsValidLocationData(ApiResponse<IpInfoData> ipInfoData)
        {
            return !string.IsNullOrEmpty(ipInfoData.Content?.city);
        }
    }
}
