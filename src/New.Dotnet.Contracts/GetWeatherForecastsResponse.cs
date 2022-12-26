namespace New.Dotnet.Contracts;

public record GetWeatherForecastsResponse
(
    IEnumerable<WeatherForecastDto> Values
);
