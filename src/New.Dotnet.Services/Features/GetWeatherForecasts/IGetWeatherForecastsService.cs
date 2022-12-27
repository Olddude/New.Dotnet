using New.Dotnet.Messaging.Contracts;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public interface IGetWeatherForecastsService
{
    Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken);
}