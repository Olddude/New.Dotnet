using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Internal;

namespace New.Dotnet.Cli.Services;

public class CreateDummyDataService : BackgroundService
{
    private readonly ILogger<CreateDummyDataService> _logger;
    private readonly IWeatherForecastRepository _repository;

    public CreateDummyDataService
    (
        ILogger<CreateDummyDataService> logger,
        IWeatherForecastRepository repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (stoppingToken.IsCancellationRequested) return;
            _logger.LogInformation("creating dummy data...");
            var data = DummyDataService.GetWeatherForecasts();
            await _repository.CreateWeatherForecastsAsync(data, stoppingToken);
            _logger.LogInformation("creating dummy data success...");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "creating dummy data failed...");
        }
    }
}