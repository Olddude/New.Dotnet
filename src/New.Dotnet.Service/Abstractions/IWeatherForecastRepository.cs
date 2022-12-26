using New.Dotnet.Service.Models;

namespace New.Dotnet.Service.Abstractions;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(int skip, int take);

    Task CreateWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts);
}