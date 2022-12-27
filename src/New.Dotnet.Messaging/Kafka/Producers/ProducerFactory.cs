using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using New.Dotnet.Messaging.Kafka.Options;

namespace New.Dotnet.Messaging.Kafka.Producers;

public class ProducerFactory
{
    private readonly ILogger<ProducerFactory> _logger;
    private readonly IOptions<KafkaOptions> _options;

    public ProducerFactory
    (
        ILogger<ProducerFactory> logger,
        IOptions<KafkaOptions> options
    )
    {
        _logger = logger;
        _options = options;
    }
    
    public IProducer<Guid, T> Build<T>()
    {
        _logger.LogInformation($"build producer for: {typeof(T)}");
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _options.Value.BootstrapServers
        };
        var producerBuilder = new ProducerBuilder<Guid, T>(producerConfig);
        return producerBuilder.Build();
    }
}