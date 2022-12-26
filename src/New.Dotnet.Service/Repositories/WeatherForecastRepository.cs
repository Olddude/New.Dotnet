using Microsoft.EntityFrameworkCore;
using New.Dotnet.Service;
using New.Dotnet.Service.Abstractions;
using New.Dotnet.Service.Models;
using New.Dotnet.Service.Repositories;

namespace New.Dotnet.Database.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ILogger<DomainEventRepository> _logger;
    private readonly ApplicationDatabaseContext _dbContext;
    
    public WeatherForecastRepository
    (
        ILogger<DomainEventRepository> logger,
        ApplicationDatabaseContext dbContext
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