namespace New.Dotnet.Contracts;

public record GetWeatherForecastsResponse
{
    public IEnumerable<WeatherForecastDto> Values { get; set; }
};