using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.Kafka.Options;

namespace New.Dotnet.Messaging.Kafka.Consumers;

public class ConsumerFactory
{
    private readonly ILogger<ConsumerFactory> _logger;
    private readonly IOptions<KafkaOptions> _options;

    public ConsumerFactory
    (
        ILogger<ConsumerFactory> logger,
        IOptions<KafkaOptions> options
    )
    {
        _logger = logger;
        _options = options;
    }
    
    public IConsumer<Guid, T> Build<T>()
    {
        _logger.LogInformation($"build consumer for: {typeof(T)}");
        var consumerConfig = new ConsumerConfig
        {
            ClientId = _options.Value.ClientId,
            BootstrapServers = _options.Value.BootstrapServers,
            GroupId = _options.Value.GroupId,
            SocketTimeoutMs = _options.Value.SocketTimeoutMs,
            SessionTimeoutMs = _options.Value.SessionTimeoutMs,
            AutoOffsetReset = (AutoOffsetReset)_options.Value.AutoOffsetReset,
        };
        var consumerBuilder = new ConsumerBuilder<Guid, T>(consumerConfig);
        return consumerBuilder.Build();
    }
}