using New.Dotnet.Messaging.Features.GetCities;

namespace New.Dotnet.Services.Features.GetCities;

public interface IGetCitiesService
{
    Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken);
}