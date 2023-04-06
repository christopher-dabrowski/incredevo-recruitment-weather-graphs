using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Services;

namespace AppService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherDataController : ControllerBase
{
    private readonly IWeatherDataRepository _weatherDataRepository;

    public WeatherDataController(IWeatherDataRepository weatherDataRepository)
    {
        _weatherDataRepository = weatherDataRepository;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<WeatherInfo>>> GetWeatherInfo(CancellationToken cancellationToken)
    {
        return (await _weatherDataRepository.GetAllWeatherForecasts(cancellationToken: cancellationToken)).ToArray();
    }
}
