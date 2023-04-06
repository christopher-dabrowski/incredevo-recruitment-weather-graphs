using FunctionApp.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<IOpenWeatherHttpClient, OpenWeatherHttpClient>();
    })
    .Build();

host.Run();
