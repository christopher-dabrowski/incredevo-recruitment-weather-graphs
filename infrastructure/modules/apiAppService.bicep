param name string
param location string

param appServiceAppPlanNme string

resource appServciePlan 'Microsoft.Web/serverfarms@2021-01-15' existing = {
  name: appServiceAppPlanNme
}

var alwayOnSupport = [ 'Basic', 'Standard', 'Premium', 'Isolated' ]

resource appServiceApp 'Microsoft.Web/sites@2021-01-15' = {
  name: name
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServciePlan.id
    httpsOnly: true
    siteConfig: {
      alwaysOn: contains(alwayOnSupport, appServciePlan.sku.tier)
    }
  }
}

output objectId string = appServiceApp.identity.principalId
