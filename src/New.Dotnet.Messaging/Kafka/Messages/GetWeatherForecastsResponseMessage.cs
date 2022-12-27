using Confluent.Kafka;
using New.Dotnet.Contracts.Features.GetWeatherForecasts;

namespace New.Dotnet.Messaging.Kafka.Messages;

public class GetWeatherForecastsResponseMessage : Message<Guid, GetWeatherForecastsResponse>
{
    private readonly Guid _id;
    private readonly GetWeatherForecastsResponseMessage _response;

    public GetWeatherForecastsResponseMessage(Guid id, GetWeatherForecastsResponseMessage response)
    {
        _id = id;
        _response = response;
    }

    public new Guid Key => _id;

    public new GetWeatherForecastsResponseMessage Value => _response;
}