using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Persistence.Abstractions;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(int skip, int take);

    Task CreateWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts);
}