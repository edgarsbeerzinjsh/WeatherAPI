using Refit;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services.Validations.IpInfoDataValidations
{
    public class IpInfoDataHasCoordinates : IIpInfoDataValidation
    {
        public bool IsValidLocationData(ApiResponse<IpInfoData> ipInfoData)
        {
            if (string.IsNullOrEmpty(ipInfoData.Content?.loc))
            {
                return false;
            }

            var coordinates = ipInfoData.Content?.loc.Split(',');

            if (coordinates.Length != 2)
            {
                return false;
            }

            if (!decimal.TryParse(coordinates[0], out var latitude) || !decimal.TryParse(coordinates[1], out var longitude))
            {
                return false;
            }

            return latitude >= -90 &&
                latitude <= 90 &&
                longitude >= -180 &&
                longitude <= 180;
        }
    }
}
