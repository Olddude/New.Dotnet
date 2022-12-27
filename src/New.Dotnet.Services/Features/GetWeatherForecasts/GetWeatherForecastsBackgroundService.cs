using Confluent.Kafka;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.Kafka.Abstractions;
using New.Dotnet.Messaging.Kafka.Options;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public class GetWeatherForecastsBackgroundService : BackgroundService
{
    private readonly ILogger<GetWeatherForecastsBackgroundService> _logger;
    private readonly IOptions<KafkaOptions> _options;
    private readonly IGetWeatherForecastsMessageHandler _handler;

    public GetWeatherForecastsBackgroundService
    (
        ILogger<GetWeatherForecastsBackgroundService> logger,
        IOptions<KafkaOptions> options,
        IGetWeatherForecastsMessageHandler handler
    )
    {
        _logger = logger;
        _options = options;
        _handler = handler;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("start consuming events");
        var consumerConfig = new ConsumerConfig
        {
            ClientId = _options.Value.ClientId,
            BootstrapServers = _options.Value.BootstrapServers,
            GroupId = _options.Value.GroupId,
            SocketTimeoutMs = _options.Value.SocketTimeoutMs,
            SessionTimeoutMs = _options.Value.SessionTimeoutMs,
            AutoOffsetReset = (AutoOffsetReset)_options.Value.AutoOffsetReset,
        };
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe(_options.Value.RequestsTopic);
    
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(cancellationToken);
            var requestMessage = consumeResult.Message;
            var responseMessage = await _handler.HandleAsync(requestMessage, cancellationToken);
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _options.Value.BootstrapServers
            };
            using var producer = new ProducerBuilder<string, string>(producerConfig).Build();
            var producerResult = await producer.ProduceAsync(_options.Value.ResponsesTopic, responseMessage, cancellationToken);
            _logger.LogInformation(producerResult.Key);
        }
    }
}