using Microsoft.AspNetCore.Mvc;
using New.Dotnet.Api.Abstractions;
using New.Dotnet.Contracts.Features.GetCities;

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