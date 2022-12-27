using New.Dotnet.Api.Abstractions;
using New.Dotnet.Contracts.Features.GetCities;

namespace New.Dotnet.Api.Services;

public class GetCitiesService : IGetCitiesService
{
    public Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}