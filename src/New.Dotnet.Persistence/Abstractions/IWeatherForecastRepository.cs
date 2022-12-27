using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Persistence.Abstractions;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(int skip, int take, CancellationToken cancellationToken);

    Task CreateWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts, CancellationToken cancellationToken);
}