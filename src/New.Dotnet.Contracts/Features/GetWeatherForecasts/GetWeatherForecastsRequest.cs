namespace New.Dotnet.Contracts.Features.GetWeatherForecasts;

public record GetWeatherForecastsRequest
{
    public int Page { get; init; }
    public int Take { get; init; }
}
