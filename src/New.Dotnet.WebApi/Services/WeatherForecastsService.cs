using New.Dotnet.Contracts;
using New.Dotnet.WebApi.Abstractions;

namespace New.Dotnet.WebApi.Services;

public class WeatherForecastsService : IWeatherForecastsService
{
    public async Task<GetWeatherForecastsResponse> GetAsync(GetWeatherForecastsRequest request, CancellationToken cancellationToken)
    {
        var response = new GetWeatherForecastsResponse
        {
            Values = new List<WeatherForecastDto>()
        };
        return response;
    }
}