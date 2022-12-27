using AutoMapper;
using New.Dotnet.Contracts.Features.GetWeatherForecasts;
using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Messaging.Kafka.Mappers;

public class WeatherForecastMapper : Profile
{
    public WeatherForecastMapper()
    {
        CreateMap<WeatherForecast, WeatherForecastDto>();

        CreateMap<WeatherForecastDto, WeatherForecast>();
    }
}