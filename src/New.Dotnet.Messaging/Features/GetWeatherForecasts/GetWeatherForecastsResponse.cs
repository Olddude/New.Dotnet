namespace New.Dotnet.Messaging.Contracts;

public record GetWeatherForecastsResponse
{
    public IEnumerable<WeatherForecastDto>? Values { get; init; }
};
