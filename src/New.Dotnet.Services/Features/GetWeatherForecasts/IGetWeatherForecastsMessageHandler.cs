using Confluent.Kafka;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public interface IGetWeatherForecastsMessageHandler
{
    Task<Message<string, string>> HandleAsync(Message<string, string> requestMessage);
}