const drawPlot = (elementId, title, text, latitudes, longitudes, onClick) => {
    const data = [{
        type: 'scattergeo',
        mode: 'markers+text',
        text: text,
        lat: latitudes,
        lon: longitudes,
        marker: {
            size: 12,
            color: '#bebada',
            line: {
                width: 1
            }
        },
        textposition: 'top',
    }];

    const layout = {
        title,
        font: {
            family: 'Droid Serif, serif',
            size: 10
        },
        titlefont: {
            size: 20
        },
        geo: {
            scope: 'europe',
            resolution: 50,
            lataxis: {
                'range': [48, 60]
            },
            lonaxis: {
                'range': [-10, 30]
            },
            showrivers: true,
            rivercolor: '#fff',
            showlakes: true,
            lakecolor: '#fff',
            showland: true,
            landcolor: '#EAEAAE',
            countrycolor: '#d3d3d3',
            countrywidth: 1.5,
            subunitcolor: '#d3d3d3'
        }
    };

    Plotly.newPlot(elementId, data, layout);

    const myPlot = document.getElementById(elementId);
    myPlot.on('plotly_click', onClick);
}

(async () => {
    const citeisResponse = await fetch("/api/WeatherData/cities");
    const cities = await citeisResponse.json();

    console.log(cities);

    const currentCitysWeatherResponse = await fetch("/api/WeatherData/currentCityWeather");
    const currentCitysWeather = await currentCitysWeatherResponse.json();

    console.log(currentCitysWeather);

    const cityNames = currentCitysWeather.map(c => c.rowKey);
    const latitudes = currentCitysWeather.map(c => c.lat);
    const longitudes = currentCitysWeather.map(c => c.lon);

    const cityTemperatures = currentCitysWeather.map(c => `${c.rowKey} ${c.temperatureInCelsius}°C`);
    const cityWind = currentCitysWeather.map(c => `${c.rowKey} ${c.windSpeed} m/s`);

    drawPlot('temperature-diagram', 'Temperature in europe', cityTemperatures, latitudes, longitudes, function (data) {
        const cityName = data.points[0].text;
        alert(`Temperature diagram in ${cityName} WIP`);
        location.assign(`/api/WeatherData/cities/${cityName.split(' ')[0]}`);
    });

    drawPlot('wind-diagram', 'Wind speed in europe', cityWind, latitudes, longitudes, function (data) {
        const cityName = data.points[0].text;
        alert(`Wind diagram for ${cityName} WIP`);
        location.assign(`/api/WeatherData/cities/${cityName.split(' ')[0]}`);
    });
})();
