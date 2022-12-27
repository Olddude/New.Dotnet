namespace New.Dotnet.Messaging.Contracts;

public record WeatherForecastDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public int Temperature { get; init; }
    public string? Message { get; init; }
    public DateTime CreatedAt { get; init; }
}