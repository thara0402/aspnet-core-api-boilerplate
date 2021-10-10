# ASP.NET Core Web API Boilerplate


## Secret Manager

```json
{
  "WebApi": {
    "SqlConnection": "SQL Connection for Secret Manager."
  }
}
```

## Entity Framwork
```PowerShell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Shop;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Infrastructure\Sql\Models
```
