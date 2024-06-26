# ASP.NET Core Web API Boilerplate

[![Build and deploy](https://github.com/thara0402/aspnet-core-api-boilerplate/actions/workflows/main_gunners-style-api.yml/badge.svg)](https://github.com/thara0402/aspnet-core-api-boilerplate/actions/workflows/main_gunners-style-api.yml)

## Setup
### SQL
- Create the database.
  - `Shop`
- Register tables and initial records using SQL Script.
  - [CreateDatabase.sql](./Database/CreateDatabase.sql) 
  - [InsertShopData.sql](./Database/InsertShopData.sql) 
### Azure Storage
- Create storage objects.
  - Table : `Product`
  - Blob Container : `sample`
  - Queue : `sample-queue`

## Secret Manager

```json
{
  "WebApi": {
    "AppInsightsConnectionString": "AppInsights ConnectionString for Secret Manager.",
    "SqlConnection": "SQL Connection for Secret Manager.",
    "CosmosConnection": "Cosmos Connection for Secret Manager.",
    "StorageConnection": "UseDevelopmentStorage=true"
  }
}
```
## Azurite
```shell
$ docker run --rm -it -p 10000:10000 -p 10001:10001 -p 10002:10002 -v c:/azurite:/data mcr.microsoft.com/azure-storage/azurite:3.30.0
```

## Use Key Vault from App Service with Azure Managed Identity
```shell
$ az webapp identity assign --name "<Web Apps Name>" --resource-group "<Resource Group Name>"
{
  "principalId": "xxx",
  "tenantId": "yyy",
  "type": "SystemAssigned",
  "userAssignedIdentities": null
}

$ az keyvault set-policy --name "<Key Vault Name>" --object-id "xxx" --secret-permissions get list
```
## Scaffolding Entity Framwork
```PowerShell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Shop;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Infrastructure\Sql\Models
```
