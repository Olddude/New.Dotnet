namespace New.Dotnet.Contracts.Features.GetCities;

public record CityDto
{
    public Guid Id { get; init; }
    
    public string? Name { get; init; }
    
    public int? Population { get; init; }
    
    public DateTime CreatedAt { get; init; }
}