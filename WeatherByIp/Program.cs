using Microsoft.EntityFrameworkCore;
using Refit;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services;
using WeatherByIp.Services.Validations;
using WeatherByIp.Services.Validations.IpInfoDataValidations;
using WeatherByIp.Services.Validations.OpenMeteoDataValidations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILocationAPI>(provider => 
    RestService.For<ILocationAPI>(builder.Configuration.GetValue<string>("LocationApiUrl")));
builder.Services.AddScoped<IWeatherAPI>(provider =>
    RestService.For<IWeatherAPI>(builder.Configuration.GetValue<string>("WeatherApiUrl")));
builder.Services.AddDbContext<WeatherByIpDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WeatherByIpData")));
builder.Services.AddTransient<IWeatherByIpDbContext, WeatherByIpDbContext>();
//builder.Services.AddScoped<IIpInfoService, IpInfoService>();
//builder.Services.AddScoped<IOpenMeteoService, OpenMeteoService>();
//builder.Services.AddScoped<IDbService<Location>, DbService<Location>>();
//builder.Services.AddScoped<IDbService<Weather>, DbService<Weather>>();
//builder.Services.AddScoped<ILocationService, LocationService>();
//builder.Services.AddScoped<IApiReturnInfoService, ApiReturnInfoService>();
//builder.Services.AddScoped<IValidationOfIpAddress, ValidationOfIpAddress>();
//builder.Services.AddScoped<IIpInfoDataValidation, IpInfoDataIsSuccessStatusCode>();
//builder.Services.AddScoped<IIpInfoDataValidation, IpInfoDataHasIp>();
//builder.Services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCity>();
//builder.Services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCountry>();
//builder.Services.AddScoped<IIpInfoDataValidation, IpInfoDataHasCoordinates>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataIsSuccessStatus>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasLatitude>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasLongitude>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasTemperature>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWindspeed>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWinddirection>();
//builder.Services.AddScoped<IOpenMeteoDataValidation, OpenMeteoDataHasWeathercode>();
builder.Services.RegisterServices();
builder.Services.RegisterValidations();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
