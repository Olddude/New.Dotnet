using Confluent.Kafka;

namespace New.Dotnet.Messaging.Kafka.Abstractions;

public interface IGetWeatherForecastsMessageHandler
{
    Task<Message<string, string>> HandleAsync(Message<string, string> requestMessage);
}