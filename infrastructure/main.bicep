@description('Base name for all resources')
param baseName string = 'fnapp${uniqueString(resourceGroup().id)}'

@description('Storage Account type')
@allowed([
  'Standard_LRS'
  'Standard_GRS'
  'Standard_RAGRS'
])
param storageAccountType string = 'Standard_LRS'

@description('Location for all resources.')
param location string = resourceGroup().location

@description('Id of the AD Tenant whre infrastructure is deployed')
param infrasturcutreTenantId string = tenant().tenantId

param applicationInsightsName string = '${baseName}-appi'
param storageAccountName string = '${take(toLower(replace(replace(baseName, '-', ''), '_', '')), 24)}sa'
param keyVaultName string = take('${baseName}-kv', 24)

param funcionHostingPlanName string = '${baseName}-func-asp'
param functionAppName string = '${baseName}-func'

param appServiceHostingPlanName string = '${baseName}-app-asp'
param appServiceName string = '${baseName}-app'
param appServiceHostingPlanSku object
param ASPNETCORE_ENVIRONMENT string = 'Production'

var functionWorkerRuntime = 'dotnet-isolated'

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: storageAccountType
  }
  kind: 'Storage'
  properties: {
    supportsHttpsTrafficOnly: true
    defaultToOAuthAuthentication: true
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: applicationInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Request_Source: 'rest'
  }
}

module functionApp './modules/azureFuncion.bicep' = {
  name: 'funcionApp'
  params: {
    location: location
    name: functionAppName
    appServciePlanName: funcionHostingPlanName
  }
}

module funcionAppSettings './modules/azureFuncionSettings.bicep' = {
  name: 'funcionAppSettings'
  params: {
    keyVaultName: keyVault.outputs.keyVaultName
    parentName: functionApp.outputs.azureFuncionName
    runtime: functionWorkerRuntime
  }
}

module appServiceHostingPlan './modules/appServicePlan.bicep' = {
  name: 'appServiceHostingPlan'
  params: {
    name: appServiceHostingPlanName
    location: location
    sku: appServiceHostingPlanSku
  }
}

module appServcie './modules/apiAppService.bicep' = {
  name: 'appServcie'
  params: {
    name: appServiceName
    location: location
    appServiceAppPlanNme: appServiceHostingPlan.outputs.name
  }
}

module appServiceSettings './modules/apiAppServiceSettings.bicep' = {
  name: 'appServiceSettings'
  params: {
    appServcieName: appServcie.outputs.name
    keyVaultName: keyVault.outputs.keyVaultName
    ASPNETCORE_ENVIRONMENT: ASPNETCORE_ENVIRONMENT
  }
}

module keyVault './modules/keyVault.bicep' = {
  name: 'keyVault'
  params: {
    name: keyVaultName
    location: location
    infrasturcutreTenantId: infrasturcutreTenantId
    storageAccountName: storageAccount.name
    applicationInsightsName: applicationInsights.name
    readers: [ functionApp.outputs.objectId, appServcie.outputs.objectId ]
  }
}
