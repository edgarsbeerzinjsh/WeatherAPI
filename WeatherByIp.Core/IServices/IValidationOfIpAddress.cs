using System.Net;

namespace WeatherByIp.Core.IServices
{
    public interface IValidationOfIpAddress
    {
        IPAddress IsValidIpAddress(string ipAddress);
    }
}
