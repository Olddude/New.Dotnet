namespace New.Dotnet.Contracts.Features.GetCities;

public record GetCitiesRequest
{
    public int Skip { get; init; }

    public int Take { get; init; }
}
