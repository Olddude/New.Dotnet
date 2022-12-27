using New.Dotnet.Messaging.Contracts;

namespace New.Dotnet.Services.Abstractions;

public interface IGetWeatherForecastsService
{
    Task<GetWeatherForecastsResponse?> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken);
}