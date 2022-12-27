using Microsoft.AspNetCore.Mvc;
using New.Dotnet.Api.Abstractions;
using New.Dotnet.Contracts.Features.GetWeatherForecasts;

namespace New.Dotnet.Api.Controllers;

[ApiController]
[Route("weather-forecasts")]
public class WeatherForecastsController : ControllerBase
{
    private readonly ILogger<WeatherForecastsController> _logger;
    private readonly IGetWeatherForecastsService _service;

    public WeatherForecastsController
    (
        ILogger<WeatherForecastsController> logger,
        IGetWeatherForecastsService service
    )
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<GetWeatherForecastsResponse?> GetAsync
    (
        [FromQuery] GetWeatherForecastsRequest request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation($"{request.Page} - {request.Take}");
        var response = await _service.GetAsync(request, cancellationToken);
        return response;
    }
}
