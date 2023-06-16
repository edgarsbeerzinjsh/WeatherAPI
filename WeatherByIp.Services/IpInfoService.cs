using Refit;
using WeatherByIp.Core.Models;
using WeatherByIp.Online.LocationDataAPI;

namespace WeatherByIp.Services
{
    public class IpInfoService : IIpInfoService
    {
        private readonly ILocationAPI _locationApi;
        private readonly IEnumerable<IIpInfoDataValidation> _validators;
        public IpInfoService(ILocationAPI location, IEnumerable<IIpInfoDataValidation> validators)
        {
            _locationApi = location;
            _validators = validators;
        }

        public async Task<Location> GetMyLocation(string ip)
        {
            try
            {
                var location = await _locationApi.GetApiLocation(ip);

                if (!_validators.All(v => v.IsValidLocationData(location)))
                {
                    return null;
                }

                var coordinates = GetCoordinates(location.Content.loc);

                return new Location()
                {
                    Ip = location.Content.ip,
                    Latitude = coordinates.latitude,
                    Longitude = coordinates.longitude,
                    City = location.Content.city,
                    Country = location.Content.country
                };
            }
            catch
            {
                return null;
            }
        }

        private (decimal latitude, decimal longitude) GetCoordinates(string location)
        {
            decimal.TryParse(location.Split(',')[0], out var latitude);
            decimal.TryParse(location.Split(',')[1], out var longitude);

            return (latitude, longitude);
        }
    }
}
