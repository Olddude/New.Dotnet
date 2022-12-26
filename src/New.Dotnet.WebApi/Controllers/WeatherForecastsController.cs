using Microsoft.AspNetCore.Mvc;
using New.Dotnet.Contracts;
using New.Dotnet.WebApi.Abstractions;

namespace New.Dotnet.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastsController : ControllerBase
{
    private readonly ILogger<WeatherForecastsController> _logger;
    private readonly IWeatherForecastsService _service;

    public WeatherForecastsController
    (
        ILogger<WeatherForecastsController> logger,
        IWeatherForecastsService service
    )
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<GetWeatherForecastsResponse> GetAsync([FromQuery] GetWeatherForecastsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{request.Page} - {request.Take}");
        var response = await _service.GetAsync(request, cancellationToken);
        return response;
    }
}
