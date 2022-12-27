using AutoMapper;
using New.Dotnet.Messaging.Contracts;
using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Services.Mappers;

internal class WeatherForecastMapper : Profile
{
    public WeatherForecastMapper()
    {
        CreateMap<WeatherForecast, WeatherForecastDto>();

        CreateMap<WeatherForecastDto, WeatherForecast>();
    }
}