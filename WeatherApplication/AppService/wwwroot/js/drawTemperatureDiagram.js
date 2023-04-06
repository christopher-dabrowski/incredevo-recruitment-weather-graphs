(async () => {
    const cityName = new URLSearchParams(window.location.search).get("city");

    const cityWeatherRequest = await fetch(`/api/WeatherData/cities/${cityName}`);
    const cityWeather = await cityWeatherRequest.json();

    const data = [
        {
            x: cityWeather.map(cw => cw.forecastTime),
            y: cityWeather.map(cw => cw.temperatureInCelsius),
            type: 'scatter',
        }
    ];

    const layout = {
        title: `Temperature in ${cityName}`,
        xaxis: {
            title: 'Time'
        },
        yaxis: {
            title: 'Temperature in °C'
        }
        //xaxis_title: 'Time',
        //yaxis_title: 'Temperature in °C'
    };

    Plotly.newPlot('diagram', data, layout);
})();
