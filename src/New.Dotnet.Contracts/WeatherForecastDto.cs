namespace New.Dotnet.Contracts;

public class WeatherForecastDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Temperature { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
}