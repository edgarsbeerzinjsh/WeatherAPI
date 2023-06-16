using System.Net;
using WeatherByIp.Core.IServices;

namespace WeatherByIp.Services.Validations
{
    public class ValidationOfIpAddress : IValidationOfIpAddress
    {
        public IPAddress IsValidIpAddress(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out var ip))
            {
                return ip;
            };

            return null;
        }
    }
}
