using New.Dotnet.Service.Abstractions;

namespace New.Dotnet.Service.Services;

public class WeatherForecastsService : BackgroundService
{
    private readonly ILogger<WeatherForecastsService> _logger;
    private readonly IDomainEventRepository _domainEventRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastsService
    (
        ILogger<WeatherForecastsService> logger,
        IDomainEventRepository domainEventRepository,
        IWeatherForecastRepository weatherForecastRepository
    )
    {
        _logger = logger;
        _domainEventRepository = domainEventRepository;
        _weatherForecastRepository = weatherForecastRepository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.CompletedTask;
            _logger.LogInformation("start consuming events");
            // var dummyWeatherForecasts = GetDummyWeatherForecasts();
            // await _weatherForecastRepository.CreateWeatherForecastsAsync(dummyWeatherForecasts);
            while (!stoppingToken.IsCancellationRequested)
            {
                continue;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}