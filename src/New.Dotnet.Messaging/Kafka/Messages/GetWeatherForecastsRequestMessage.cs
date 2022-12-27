using Confluent.Kafka;
using New.Dotnet.Contracts.Features.GetWeatherForecasts;

namespace New.Dotnet.Messaging.Kafka.Messages;

public class GetWeatherForecastsRequestMessage : Message<Guid, GetWeatherForecastsRequest>
{
    private readonly Guid _id;
    private readonly GetWeatherForecastsRequest _request;

    public GetWeatherForecastsRequestMessage(Guid id, GetWeatherForecastsRequest request)
    {
        _id = id;
        _request = request;
    }

    public new Guid Key => _id;

    public new GetWeatherForecastsRequest Value => _request;
}