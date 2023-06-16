using System.Net;
using FluentAssertions;
using WeatherByIp.Services.Validations;

namespace WeatherByIp.Tests
{
    public class ValidationOfIpAddressTests
    {
        private ValidationOfIpAddress _validation;

        [SetUp]
        public void Setup()
        {
            _validation = new ValidationOfIpAddress();
        }

        [Test]
        public void IsValidIpAddress_ValidIp_IpReturned()
        {
            var ip = "11.11.11.11";

            var result = _validation.IsValidIpAddress(ip);
            
            result.Should().NotBeNull();
            result.Should().Be(IPAddress.Parse(ip));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("someip")]
        public void IsValidIpAddress_NotValidIp_NullReturned(string ip)
        {
            var result = _validation.IsValidIpAddress(ip);

            result.Should().BeNull();
        }
    }
}
