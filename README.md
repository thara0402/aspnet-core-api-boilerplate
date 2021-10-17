# ASP.NET Core Web API Boilerplate


## Secret Manager

```json
{
  "WebApi": {
    "SqlConnection": "SQL Connection for Secret Manager.",
    "CosmosConnection": "Cosmos Connection for Secret Manager."
  }
}
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
