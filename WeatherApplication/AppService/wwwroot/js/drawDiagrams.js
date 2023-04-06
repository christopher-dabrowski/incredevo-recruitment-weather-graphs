(async () => {
    const weatherDataResponse = await fetch("/api/WeatherData/all");
    const weatherData = await weatherDataResponse.json();

    console.log(weatherData);

    const citeisResponse = await fetch("/api/WeatherData/cities");
    const cities = await citeisResponse.json();

    console.log(cities);

    const cityNames = cities.map(c => c.name);
    const latitudes = cities.map(c => c.lat)
    const longitudes = cities.map(c => c.lon)

    const data = [{
        type: 'scattergeo',
        mode: 'markers+text',
        text: cityNames,
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
        title: 'Temperature in europe',
        font: {
            family: 'Droid Serif, serif',
            size: 10
        },
        titlefont: {
            size: 16
        },
        geo: {
            scope: 'europe',
            resolution: 50,
            lataxis: {
                'range': [45, 60]
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

    Plotly.newPlot('diagram', data, layout);

    const myPlot = document.getElementById('diagram');
    myPlot.on('plotly_click', function (data) {
        const cityName = data.points[0].text;
        alert(`Temperature diagram in ${cityName} WIP`)
    });
})();
