using Microsoft.Extensions.DependencyInjection;
using WeatherByIp.Core.IServices;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services.Validations.IpInfoDataValidations;
using WeatherByIp.Services.Validations.OpenMeteoDataValidations;
using WeatherByIp.Services.Validations;
using WeatherByIp.Core.Models;

namespace WeatherByIp.Services
{
    public static class DependencyUtils
    {
        public static void RegisterValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidationOfIpAddress, ValidationOfIpAddress>();
            services.AddScoped<IIpInfoDataValidation, IpInfoDataIsSuccessStatusCode>();
            services.AddScoped<IIpInfoDataValidation, IpInfoDataHasIp>();
            services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCity>();
            services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCountry>();
            services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCoordinates>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataIsSuccessStatus>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasLatitude>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasLongitude>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasTemperature>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWindspeed>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWinddirection>();
            services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWeathercode>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IIpInfoService, IpInfoService>();
            services.AddScoped<IOpenMeteoService, OpenMeteoService>();
            services.AddScoped<IDbService<Location>, DbService<Location>>();
            services.AddScoped<IDbService<Weather>, DbService<Weather>>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IApiReturnInfoService, ApiReturnInfoService>();
        }
    }
}
