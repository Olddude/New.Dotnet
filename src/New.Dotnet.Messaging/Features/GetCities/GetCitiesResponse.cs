namespace New.Dotnet.Messaging.Features.GetCities;

public record GetCitiesResponse
{
    public ICollection<CityDto> Values { get; init; }
}