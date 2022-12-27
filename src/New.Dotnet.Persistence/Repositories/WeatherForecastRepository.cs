using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Context;
using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Persistence.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ILogger<DomainEventRepository> _logger;
    private readonly DatabaseContext _dbContext;
    
    public WeatherForecastRepository
    (
        ILogger<DomainEventRepository> logger,
        DatabaseContext dbContext
    )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _dbContext.WeatherForecasts.Skip(skip).Take(take).ToListAsync(cancellationToken);
    }

    public async Task CreateWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.WeatherForecasts.AddRangeAsync(weatherForecasts, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"{result}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "could not create weather forecasts...");
        }
    }
}