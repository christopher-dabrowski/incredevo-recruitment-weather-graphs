﻿(async () => {
    const cityName = new URLSearchParams(window.location.search).get("city");

    const cityWeatherRequest = await fetch(`/api/WeatherData/cities/${cityName}`);
    const cityWeather = await cityWeatherRequest.json();

    console.log(cityWeather);

    const data = [
        {
            x: cityWeather.map(cw => cw.forecastTime),
            y: cityWeather.map(cw => cw.wind),
            type: 'scatter',
            mode: 'markers'
        }
    ];

    const layout = {
        title: `Wind spped in ${cityName}`,
        xaxis_title: 'Time',
        yaxis_title: 'Wind in m/s'
    };

    Plotly.newPlot('diagram', data, layout);
})();
