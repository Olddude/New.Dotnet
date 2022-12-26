using New.Dotnet.Contracts;

namespace New.Dotnet.WebApi.Abstractions;

public interface IWeatherForecastsService
{
    Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken);
}