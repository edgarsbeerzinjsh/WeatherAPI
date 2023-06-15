using Microsoft.EntityFrameworkCore;
using Refit;
using WeatherByIp.Core.IServices;
using WeatherByIp.Core.Models;
using WeatherByIp.Data;
using WeatherByIp.Online.LocationDataAPI;
using WeatherByIp.Online.WeatherDataAPI;
using WeatherByIp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILocationAPI>(provider => 
    RestService.For<ILocationAPI>(builder.Configuration.GetValue<string>("LocationApiUrl")));
builder.Services.AddScoped<IIpInfoService, IpInfoService>();
builder.Services.AddScoped<IWeatherAPI>(provider =>
    RestService.For<IWeatherAPI>(builder.Configuration.GetValue<string>("WeatherApiUrl")));
builder.Services.AddScoped<IOpenMeteoService, OpenMeteoService>();
builder.Services.AddDbContext<WeatherByIpDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WeatherByIpData")));
builder.Services.AddTransient<IWeatherByIpDbContext, WeatherByIpDbContext>();
builder.Services.AddScoped<IDbService<Location>, DbService<Location>>();
builder.Services.AddScoped<IDbService<Weather>, DbService<Weather>>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IApiReturnInfoService, ApiReturnInfoService>();

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
