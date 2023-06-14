using System.Net;
using WeatherByIp.Core.Models;

namespace WeatherByIp.LocationDataAPI
{
    public class IpInfoService : IIpInfoService
    {
        private readonly ILocationAPI _locationApi;
        public IpInfoService(ILocationAPI location)
        {
            _locationApi = location;
        }

        public async Task<Location> GetLocation(string ip)
        {
            var location = await _locationApi.GetLocation(ip);
            if (location.IsSuccessStatusCode)
            {
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

            return null;
        }

        private (decimal latitude, decimal longitude) GetCoordinates(string location)
        {
            decimal.TryParse(location.Split(',')[0], out var latitude);
            decimal.TryParse(location.Split(',')[1], out var longitude);

            return (latitude, longitude);
        }
    }
}
