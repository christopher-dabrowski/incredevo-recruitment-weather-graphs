# IncreDevo recruitment task - Weather Data Application

[![Deploy Azure infrastructure](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployInfrastructure.yml/badge.svg)](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployInfrastructure.yml)

[![Deploy ASP App](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployAppService.yml/badge.svg)](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployAppService.yml)

[![Deploy Funcion App](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployAzureFuncion.yml/badge.svg)](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions/workflows/deployAzureFuncion.yml)

This is my attempt at solving the second recruitment task for the [IncreDevo company](https://incredevo.com/)

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Task description](#task-description)
- [Solution](#solution)
  - [Architecture](#architecture)
    - [IoC](#ioc)
  - [Time tracking](#time-tracking)
  - [CI/CD](#cicd)
  - [Solution showcase](#solution-showcase)
    - [Application in action](#application-in-action)
    - [Current wind and temperature graphs](#current-wind-and-temperature-graphs)
    - [Temperature in the last hour diagram](#temperature-in-the-last-hour-diagram)
    - [Wind in the last hour diagram](#wind-in-the-last-hour-diagram)
    - [Azure resources](#azure-resources)
    - [Storage account content](#storage-account-content)
    - [GitHub Action runs](#github-action-runs)
  - [Notes](#notes)


## Task description

Must use:

- ASP.NET CORE MVC (6)
- C#
- JavaScript

Achieve:

Using any public weather API receive data (country, city, temperature, clouds, wind speed) from at least 10 cities in 5 countries  
with periodical update 1/min,
store this data in the database  
and show the 2 graphs:  

- min temperature (Country\City\Temperature\Last update time)
- highest wind speed (Country\City\Wind Speed\Last update time)
- temperature & wind speed trend for last 2 hours on click for both previous graphs

## Solution

Description of my solution to the presented problem.

### Architecture

![Architecture diagram](docs/Architecture.drawio.png)

#### IoC

I've used [Bicep](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview) to define IoC. The IoC approach allows me to have documented and consistent infrastructure.
I've chosen Bicep technology as it's easy to run from any environment that has access to Azure.

The Bicep code can be found in the [infrastructure](infrastructure) directory.

The Bicep resource and module dependencies diagram looks as follows:

![Bicep resources](./docs/bicep_modules.png)

### Time tracking

For time tracking I'm using [Clockify](https://clockify.me/) and I'm tracking time for each task using GitHub issues.  
I'll add the final time report when I finish the task.

Total time spent on project: 08:38:13

Time breakdown per task in completion order:  
_format: \#{gh-issue} {name} - {time spent}_

- #1 Repo setup - 00:11:22
- #2 Describe tasks - 00:06:13
- #8 Explore API - 00:36:17
- #4 IoC - 00:20:16
- #5 CI/CD for IoC - 00:27:21
- #6 Scaffold ASP project - 00:05:00
- #13 CI/CD for Azure Funcion - 00:33:47
- #10 Setup data fetching - 02:21:32
- #12 Display weather trends graphs - 03:15:30
- #15 Describe the solution - 00:40:55

### CI/CD

To implement an automated deployment process I've used GitHub Actions.  
Specific actions are triggered when changes are pushed to a specific repository path or can be triggered manually.  
Actions definitions can be found in the [.github/workflows](.github/workflows) directory.

### Solution showcase

The app can be viewed on Azure <https://kd-dev-incre-weather-app.azurewebsites.net/>.

#### Application in action

![Application demo](docs/application_demo.gif)

#### Current wind and temperature graphs

![Current wind and temperature graphs](docs/wind_and_temp.png)

#### Temperature in the last hour diagram

![A temperature in the last hour diagram](docs/temperature_diagram.png)

#### Wind in the last hour diagram

![Wind in the last hour diagram](docs/wind_diagram.png)

#### Azure resources

![Resource Group](docs/rg.png)

#### Storage account content

![Storage account content](docs/storage_account_contnet.png)

#### GitHub Action runs

The runs can be viewed in the [Actions repo tab](https://github.com/christopher-dabrowski/incredevo-recruitment-weather-graphs/actions)

![deploy infrastructure action runs](docs/deploy_infrastructure_action_runs.png)

![deploy Azure Function runs](docs/deploy_af_runs.png)

![deploy App Service runs](docs/deploy_app_servcie_runs.png)

### Notes

Noteworthy decisions and aspects of the implementation and project configuration or work methodology.

- I've decided to use Azure Table Storage for the database, as I don't need to model relations between entities and Table Storage is significantly cheaper than a SQL database
