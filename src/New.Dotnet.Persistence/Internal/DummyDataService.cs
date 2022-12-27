using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Persistence.Internal;

public static class DummyDataService
{
    public static IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        var messages = new[]
        {
            "Sunny", "Freezing", "Cloudy", "Rainy", "Poppy"
        };
        return Enumerable.Range(0, 1000).Select(_ =>
        {
            var date = DateTime.UtcNow.AddDays(_);
            var temp = new Random().Next(-71, 55);
            var messageIndex = new Random().Next(0, messages.Length - 1);
            return new WeatherForecast(date, temp, messages[messageIndex]);
        });
    }
}
