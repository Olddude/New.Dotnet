using New.Dotnet.Messaging.Features.GetCities;

namespace New.Dotnet.Services.Abstractions;

public interface IGetCitiesService
{
    Task<GetCitiesResponse?> GetAsync(GetCitiesRequest request, CancellationToken cancellationToken);
}