namespace New.Dotnet.Contracts;

public record WeatherForecastDto
(
    Guid Id,
    DateTime Date,
    int Temperature,
    string Message,
    DateTime CreatedAt
);