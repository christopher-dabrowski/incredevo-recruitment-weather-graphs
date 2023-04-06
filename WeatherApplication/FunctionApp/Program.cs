using FunctionApp.HttpClients;
using functionApp.services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<IOpenWeatherHttpClient, OpenWeatherHttpClient>();
        services.AddTransient<IWeatherDataRepository, WeatherDataRepository>();
        services.AddTransient<IClock, Clock>();
        services.AddTransient<ICityRepository, CityRepository>();
    })
    .Build();

host.Run();
