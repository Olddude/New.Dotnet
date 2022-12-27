namespace New.Dotnet.Contracts.Features.GetCities;

public record GetCitiesResponse
{
    public ICollection<CityDto> Values { get; init; }
}