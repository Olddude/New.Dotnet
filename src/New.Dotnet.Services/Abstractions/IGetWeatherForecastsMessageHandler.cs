using Confluent.Kafka;

namespace New.Dotnet.Services.Abstractions;

public interface IGetWeatherForecastsMessageHandler
{
    Task<Message<string, string>> HandleAsync(Message<string, string> requestMessage);
}