# NuGet Package Upgrade Plan

## Overview

**Target**: Upgrade all 13 assessed NuGet packages in `WebApp` to their latest stable recommended versions and migrate any affected source usages.
**Scope**: One .NET 11 project.

## Tasks

### 01-azure-client-packages: Upgrade Azure client packages

Update the `WebApp` package references for Azure.Data.Tables (12.13.0), Azure.Extensions.AspNetCore.Configuration.Secrets (1.5.2), Azure.Identity (1.21.0), Azure.Storage.Blobs (12.30.2), Azure.Storage.Queues (12.28.2), and Microsoft.Azure.Cosmos (3.64.0) in `WebApp.csproj`. The assessment reports no source-breaking changes for the table, secrets, blob, queue, and Cosmos packages. For Azure.Identity, migrate any actual usages of the 55 types reported removed in [Azure.Identity.apidiff.md](apidiff/Azure.Identity.apidiff.md); quick assessment did not identify call sites, so use build feedback to locate affected code and verify whether reported removals are used.

**Done when**: All six package references match their recommended versions, applicable Azure.Identity usages compile against the new API, and `WebApp` restores and builds.

---

### 02-application-insights: Upgrade Application Insights

Update Microsoft.ApplicationInsights.AspNetCore to 3.2.0 in `WebApp.csproj`. Migrate any usages affected by the removed types, `ApplicationInsightsServiceOptions` members, and middleware extensions documented in [Microsoft.ApplicationInsights.AspNetCore.apidiff.md](apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md). Since this was a quick assessment, use build feedback to locate concrete usages rather than assuming every listed API is referenced.

**Done when**: The package reference matches 3.2.0, used APIs are migrated to supported equivalents (or confirmed unused), and `WebApp` builds with its telemetry configuration and behavior intact.

---

### 03-openapi-and-remaining-packages: Upgrade OpenAPI and remaining packages

Update the remaining assessed references in `WebApp.csproj`: AutoMapper (17.0.0), Microsoft.EntityFrameworkCore.SqlServer (11.0.0), Microsoft.EntityFrameworkCore.Tools (11.0.0), Microsoft.OpenApi (3.11.1), Newtonsoft.Json (13.0.4), and Swashbuckle.AspNetCore (11.0.1). Preserve the existing EF Tools asset settings. No source-breaking changes were reported for AutoMapper, EF Core, Newtonsoft.Json, or Swashbuckle.AspNetCore. For Microsoft.OpenApi, migrate affected content dictionaries and callers to the `IOpenApiMediaType` API, replace removed `Content` setters as needed, and address removed `OpenApiXml.Attribute` / `Wrapped` members, following [Microsoft.OpenApi.apidiff.md](apidiff/Microsoft.OpenApi.apidiff.md). Verify the OpenAPI/Swashbuckle combination together.

**Done when**: All six references match their recommended versions, applicable Microsoft.OpenApi usages compile against the new signatures, and `WebApp` restores and builds.
