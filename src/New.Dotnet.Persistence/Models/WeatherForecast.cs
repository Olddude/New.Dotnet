using System.ComponentModel.DataAnnotations;

namespace New.Dotnet.Persistence.Models;

public class WeatherForecast
{
    public WeatherForecast(DateTime date, int temperature, string message)
    {
        Id = Guid.NewGuid();
        Date = date;
        Temperature = temperature;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }
    
    [Required]
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int Temperature { get; set; }
    
    [Required]
    public string Message { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}