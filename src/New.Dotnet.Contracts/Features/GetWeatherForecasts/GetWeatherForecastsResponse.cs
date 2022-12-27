namespace New.Dotnet.Contracts.Features.GetWeatherForecasts;

public record GetWeatherForecastsResponse
{
    public IEnumerable<WeatherForecastDto>? Values { get; init; }
};
