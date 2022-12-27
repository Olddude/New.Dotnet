using New.Dotnet.Messaging.Features.GetCities;

namespace New.Dotnet.Services.Features.GetCities;

public class GetCitiesService : IGetCitiesService
{
    public Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}