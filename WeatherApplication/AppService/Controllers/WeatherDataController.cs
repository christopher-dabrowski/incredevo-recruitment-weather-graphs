﻿using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Services;
using System.Threading;

namespace AppService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherDataController : ControllerBase
{
    private readonly IWeatherDataRepository _weatherDataRepository;
    private readonly ICityRepository _cityRepository;

    public WeatherDataController(IWeatherDataRepository weatherDataRepository, ICityRepository cityRepository)
    {
        _weatherDataRepository = weatherDataRepository;
        _cityRepository = cityRepository;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<WeatherInfo>>> GetWeatherInfo(CancellationToken cancellationToken)
    {
        return (await _weatherDataRepository.GetAllWeatherForecasts(cancellationToken: cancellationToken)).ToArray();
    }

    [HttpGet("cities")]
    public async Task<ActionResult<IEnumerable<CityInfo>>> ListCities(CancellationToken cancellation)
    {
        return (await _cityRepository.GetAllCities(cancellation)).ToArray();
    }

    [HttpGet("currentCityWeather")]
    public async Task<ActionResult<IEnumerable<CityCurrentWeatherInfo>>> GetCurrentCityWeather(CancellationToken cancellationToken) =>
        (await _weatherDataRepository.GetCurrentWeatherForAllCities(cancellationToken)).ToArray();

    [HttpGet("cities/{cityName}")]
    public async Task<ActionResult<IEnumerable<WeatherInfo>>> GetWeatherInCity(string cityName, CancellationToken cancellationToken)
    {
        return (await _weatherDataRepository.GetWeatherInCity(cityName, from: DateTimeOffset.Now.AddHours(-1), cancellationToken: cancellationToken))
            .OrderBy(cw => cw.ForecastTime).ToArray();
    }
}
