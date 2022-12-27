using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.Contracts;
using New.Dotnet.Messaging.Options;
using New.Dotnet.Services.Abstractions;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public class GetWeatherForecastsService : IGetWeatherForecastsService
{
    private readonly ILogger<GetWeatherForecastsService> _logger;
    private readonly IOptions<KafkaOptions> _options;

    public GetWeatherForecastsService
    (
        ILogger<GetWeatherForecastsService> logger,
        IOptions<KafkaOptions> options
    )
    {
        _logger = logger;
        _options = options;
    }

    public async Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("getting weather forecasts async");
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
        consumer.Subscribe(_options.Value.ResponsesTopic);

        var requestMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(request)
        };
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _options.Value.BootstrapServers
        };
        using var producer = new ProducerBuilder<string, string>(producerConfig).Build();
        var producerResult = await producer.ProduceAsync(_options.Value.RequestsTopic, requestMessage, cancellationToken);
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumerResult = consumer.Consume(cancellationToken);
            var key = consumerResult.Message.Key;
            if (key == requestMessage.Key)
            {
                return JsonSerializer.Deserialize<GetWeatherForecastsResponse>(consumerResult.Message.Value);
            }
        }

        return null;
    }
}