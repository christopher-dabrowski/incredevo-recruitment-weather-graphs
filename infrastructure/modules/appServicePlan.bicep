param name string
param location string
param sku object

resource appServicePlan 'Microsoft.Web/serverfarms@2021-01-15' = {
  name: name
  location: location
  sku: sku
}

output name string = appServicePlan.name
