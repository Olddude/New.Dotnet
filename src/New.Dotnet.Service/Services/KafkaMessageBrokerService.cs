using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using New.Dotnet.Contracts;
using New.Dotnet.Service.Abstractions;
using New.Dotnet.Service.Models;
using New.Dotnet.Service.Options;

namespace New.Dotnet.Service.Services;

public class KafkaMessageBrokerService : BackgroundService
{
    private readonly ILogger<KafkaMessageBrokerService> _logger;
    private readonly IOptions<KafkaOptions> _options;
    private readonly IDomainEventRepository _domainEventRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IConsumer<string, string> _consumer;
    private readonly IProducer<string, string> _producer;

    public KafkaMessageBrokerService
    (
        ILogger<KafkaMessageBrokerService> logger,
        IOptions<KafkaOptions> options,
        IDomainEventRepository domainEventRepository,
        IWeatherForecastRepository weatherForecastRepository
    )
    {
        _logger = logger;
        _options = options;
        _domainEventRepository = domainEventRepository;
        _weatherForecastRepository = weatherForecastRepository;
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
            var requestKey = consumeResult.Message.Key;
            var requestValue = consumeResult.Message.Value;
            var requestContract = JsonSerializer.Deserialize<GetWeatherForecastsRequest>(requestValue);
            if (requestContract == null) continue;
            var weatherForecasts = await _weatherForecastRepository.GetWeatherForecastsAsync(requestContract.Page, requestContract.Take);
            var responseContract = new GetWeatherForecastsResponse(weatherForecasts.Select(_ =>
            {
                var dto = new WeatherForecastDto(_.Id, _.Date, _.Temperature, _.Message, _.CreatedAt);
                return dto;
            }));
            var responseMessage = new Message<string, string>
            {
                Key = requestKey,
                Value = JsonSerializer.Serialize(responseContract)
            };
            
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
    
    private static IEnumerable<WeatherForecast> GetDummyWeatherForecasts()
    {
        var messages = new[]
        {
            "Sunny", "Freezing", "Cloudy", "Rainy", "Poppy"
        };
        return Enumerable.Range(0, 1000).Select(_ =>
        {
            var date = DateTime.UtcNow.AddDays(_);
            var temp = new Random().Next(-71, 55);
            var messageIndex = new Random().Next(0, messages.Length - 1);
            return new WeatherForecast(date, temp, messages[messageIndex]);
        });
    }
}