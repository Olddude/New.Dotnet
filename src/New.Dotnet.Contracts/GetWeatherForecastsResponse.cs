namespace New.Dotnet.Contracts;

public record GetWeatherForecastsResponse
{
    public ICollection<WeatherForecastDto> Values { get; set; }
};