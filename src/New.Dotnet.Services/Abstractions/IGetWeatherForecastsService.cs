using New.Dotnet.Contracts.Features.GetWeatherForecasts;

namespace New.Dotnet.Services.Abstractions;

public interface IGetWeatherForecastsService
{
    Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken);
}