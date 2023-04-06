﻿(async () => {
    const weatherDataResponse = await fetch("/api/WeatherData/all");
    const weatherData = await weatherDataResponse.json();

    console.log(weatherData);

    const data = [{
        type: 'scattergeo',
        mode: 'markers+text',
        text: [
            'Berlin 15C', 'Toronto', 'Vancouver', 'Calgary', 'Edmonton',
            'Ottawa', 'Halifax', 'Victoria', 'Winnepeg', 'Regina'
        ],
        lat: [
            52.5170365, 43.4, 49.13, 51.1, 53.34, 45.24,
            44.64, 48.25, 49.89, 50.45
        ],
        lon: [
            13.3888599, -79.24, -123.06, -114.1, -113.28,
            -75.43, -63.57, -123.21, -97.13, -104.6
        ],
        marker: {
            size: 12,
            color: [
                '#bebada', '#fdb462', '#fb8072', '#d9d9d9', '#bc80bd',
                '#b3de69', '#8dd3c7', '#80b1d3', '#fccde5', '#ffffb3'
            ],
            line: {
                width: 1
            }
        },
        name: 'Canadian cities',
        textposition: [
            'top', 'top left', 'top center', 'bottom right', 'top right',
            'top left', 'bottom right', 'bottom left', 'top right', 'top right'
        ],
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
            //scope: 'world',
            resolution: 50,
            lataxis: {
                'range': [35, 60]
            },
            lonaxis: {
                'range': [-10, 25]
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
})();
