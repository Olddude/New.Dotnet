using New.Dotnet.Messaging.Features.GetCities;
using New.Dotnet.Services.Abstractions;

namespace New.Dotnet.Services.Features.GetCities;

public class GetCitiesService : IGetCitiesService
{
    public Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}