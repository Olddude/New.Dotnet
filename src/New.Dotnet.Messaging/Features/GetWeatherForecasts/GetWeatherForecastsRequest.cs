namespace New.Dotnet.Messaging.Contracts;

public record GetWeatherForecastsRequest
{
    public int Page { get; init; }
    public int Take { get; init; }
}
