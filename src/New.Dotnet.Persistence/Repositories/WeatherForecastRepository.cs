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

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(int skip, int take)
    {
        var results = await _dbContext.WeatherForecasts.Skip(skip).Take(take).ToListAsync();
        return results;
    }

    public async Task CreateWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts)
    {
        await _dbContext.WeatherForecasts.AddRangeAsync(weatherForecasts);
        await _dbContext.SaveChangesAsync();
    }
}