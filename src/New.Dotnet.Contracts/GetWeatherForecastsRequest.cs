namespace New.Dotnet.Contracts;

public record GetWeatherForecastsRequest
(
    int Page,
    int Take
);