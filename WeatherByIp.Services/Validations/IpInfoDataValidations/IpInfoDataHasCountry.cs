using Refit;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services.Validations.IpInfoDataValidations
{
    public class IpInfoDataHasCountry : IIpInfoDataValidation
    {
        public bool IsValidLocationData(ApiResponse<IpInfoData> ipInfoData)
        {
            return !string.IsNullOrEmpty(ipInfoData.Content?.country);
        }
    }
}
