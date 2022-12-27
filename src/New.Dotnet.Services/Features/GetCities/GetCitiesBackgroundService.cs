using MassTransit;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.RabbitMq.Options;

namespace New.Dotnet.Services.Features.GetCities;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly ILogger<RabbitMqBackgroundService> _logger;
    private readonly IOptions<RabbitMqOptions> _options;
    private readonly IBus _bus;

    public RabbitMqBackgroundService
    (
        ILogger<RabbitMqBackgroundService> logger,
        IOptions<RabbitMqOptions> options,
        IBus bus
    )
    {
        _logger = logger;
        _options = options;
        _bus = bus;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("start consuming events");
        while (!stoppingToken.IsCancellationRequested)
        {
            break;
        }
        throw new NotImplementedException();
    }
}