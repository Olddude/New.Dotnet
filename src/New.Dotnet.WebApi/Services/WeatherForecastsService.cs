using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using New.Dotnet.Contracts;
using New.Dotnet.MessageBus.Options;
using New.Dotnet.WebApi.Abstractions;

namespace New.Dotnet.WebApi.Services;

public class WeatherForecastsService : IWeatherForecastsService
{
    private readonly ILogger<WeatherForecastsService> _logger;
    private readonly IOptions<KafkaOptions> _options;
    private readonly IConsumer<string, string> _consumer;
    private readonly IProducer<string, string> _producer;
    
    public WeatherForecastsService
    (
        ILogger<WeatherForecastsService> logger,
        IOptions<KafkaOptions> options
    )
    {
        _logger = logger;
        _options = options;
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

    public async Task<GetWeatherForecastsResponse> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("getting weather forecasts async");
        _consumer.Subscribe(_options.Value.ResponsesTopic);

        var requestMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(request)
        };
        var producerResult =
            await _producer.ProduceAsync(_options.Value.RequestsTopic, requestMessage, cancellationToken);

        GetWeatherForecastsResponse response = null;
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumerResult = _consumer.Consume(cancellationToken);
            var key = consumerResult.Message.Key;
            if (key == requestMessage.Key)
            {
                response = JsonSerializer.Deserialize<GetWeatherForecastsResponse>(consumerResult.Message.Value);
                break;
            }
        }

        return response;
    }
}