using New.Dotnet.Contracts.Features.GetCities;

namespace New.Dotnet.Api.Abstractions;

public interface IGetCitiesService
{
    Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken);
}