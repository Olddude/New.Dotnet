using Microsoft.AspNetCore.Mvc;
using New.Dotnet.Messaging.Features.GetCities;
using New.Dotnet.Services.Features.GetCities;

namespace New.Dotnet.Api.Controllers;

[ApiController]
[Route("cities")]
public class CitiesController : ControllerBase
{
    private readonly ILogger<WeatherForecastsController> _logger;
    private readonly IGetCitiesService _service;

    public CitiesController
    (
        ILogger<WeatherForecastsController> logger,
        IGetCitiesService service
    )
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<GetCitiesResponse?> GetAsync
    (
        [FromQuery] GetCitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}