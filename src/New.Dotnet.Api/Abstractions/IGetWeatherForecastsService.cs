using New.Dotnet.Contracts.Features.GetWeatherForecasts;

namespace New.Dotnet.Api.Abstractions;

public interface IGetWeatherForecastsService
{
    Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken);
}