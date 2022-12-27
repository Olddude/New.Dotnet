using Confluent.Kafka;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.Options;
using New.Dotnet.Persistence.Abstractions;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public class GetWeatherForecastsBackgroundService : BackgroundService
{
    private readonly ILogger<GetWeatherForecastsBackgroundService> _logger;
    private readonly IOptions<KafkaOptions> _options;
    private readonly IWeatherForecastRepository _repository;
    private readonly IConsumer<string, string> _consumer;
    private readonly IProducer<string, string> _producer;
    
    public GetWeatherForecastsBackgroundService
    (
        ILogger<GetWeatherForecastsBackgroundService> logger,
        IOptions<KafkaOptions> options,
        IWeatherForecastRepository repository
    )
    {
        _logger = logger;
        _options = options;
        _repository = repository;
        _consumer = new ConsumerBuilder<string, string>(new ConsumerConfig
        {
            ClientId = _options.Value.ClientId,
            BootstrapServers = _options.Value.BootstrapServers,
            GroupId = _options.Value.GroupId,
            SocketTimeoutMs = _options.Value.SocketTimeoutMs,
            SessionTimeoutMs = _options.Value.SessionTimeoutMs,
            AutoOffsetReset = (AutoOffsetReset)_options.Value.AutoOffsetReset,
        }).Build();
        _producer = new ProducerBuilder<string, string>(new ProducerConfig
        {
            BootstrapServers = _options.Value.BootstrapServers
        }).Build();
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("start consuming events");
        _consumer.Subscribe(_options.Value.RequestsTopic);
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = _consumer.Consume(cancellationToken);
            var requestMessage = consumeResult.Message;
            var requestHandler = new GetWeatherForecastsMessageHandler(_repository);
            var responseMessage = await requestHandler.HandleAsync(requestMessage);
            var producerResult = await _producer.ProduceAsync(_options.Value.ResponsesTopic, responseMessage, cancellationToken);
            _logger.LogInformation(producerResult.Key);
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        _consumer.Dispose();
        _producer.Dispose();
    }
}