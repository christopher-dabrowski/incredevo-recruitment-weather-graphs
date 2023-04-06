param appServcieName string
param ASPNETCORE_ENVIRONMENT string = 'Production'

param keyVaultName string

resource appServcie 'Microsoft.Web/sites@2021-01-15' existing = {
  name: appServcieName
}

resource appServiceConfig 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: appServcie
  name: 'appsettings'
  properties: {
    // APPLICATIONINSIGHTS_CONNECTION_STRING: '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=StorageAccount--ConnectionString)'
    WeatherDataStorage: '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=StorageAccount--ConnectionString)'
    ApplicationInsightsAgent_EXTENSION_VERSION: '~2'
    ASPNETCORE_ENVIRONMENT: ASPNETCORE_ENVIRONMENT
  }
}

var storageAccountConnectionString = '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=StorageAccount--ConnectionString)'

resource connectionStrings 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: appServcie
  name: 'connectionstrings'
  properties: {
    WeatherDataStorage: {
      value: storageAccountConnectionString
      type: 'Custom'
    }
  }
}
