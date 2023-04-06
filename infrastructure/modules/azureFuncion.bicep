param name string
param location string

param appServciePlanName string

resource hostingPlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: appServciePlanName
  location: location
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
  properties: {}
}

resource functionApp 'Microsoft.Web/sites@2022-03-01' = {
  name: name
  location: location
  kind: 'functionapp'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: hostingPlan.id
    siteConfig: {
      ftpsState: 'FtpsOnly'
      minTlsVersion: '1.2'
    }
    httpsOnly: true
  }
}

output azureFuncionName string = functionApp.name
output objectId string = functionApp.identity.principalId
