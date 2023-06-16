using System.Net;
using System.Net.NetworkInformation;
using WeatherByIp.Core.IServices;
using WeatherByIp.Online;

namespace WeatherByIp.Services.Validations
{
    public class ValidationOfIpAddress : IValidationOfIpAddress
    {
        public IPAddress IsValidIpAddress(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out var ip))
            {
                return ip;
            }

            return null;
        }
    }
}
