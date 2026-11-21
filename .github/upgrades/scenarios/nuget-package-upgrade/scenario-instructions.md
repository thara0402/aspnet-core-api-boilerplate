# NuGet Package Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Scope**: Solution aspnet-core-api-boilerplate.sln (WebApp)
- **Packages**: AutoMapper, Azure.Data.Tables, Azure.Extensions.AspNetCore.Configuration.Secrets, Azure.Identity, Azure.Storage.Blobs, Azure.Storage.Queues, Microsoft.ApplicationInsights.AspNetCore, Microsoft.Azure.Cosmos, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools, Microsoft.OpenApi, Newtonsoft.Json, Swashbuckle.AspNetCore
- **Version Policy**: Latest Stable (exclude prerelease versions)
- **API Scan**: Quick assessment

## Source Control
- **Source Branch**: upgrade-dotnet-11
- **Working Branch**: nuget-package-upgrade
- **Pending Changes Action**: Stash
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Execution Constraints
- **Chosen package versions**: AutoMapper 17.0.0; Azure.Data.Tables 12.13.0; Azure.Extensions.AspNetCore.Configuration.Secrets 1.5.2; Azure.Identity 1.21.0; Azure.Storage.Blobs 12.30.2; Azure.Storage.Queues 12.28.2; Microsoft.ApplicationInsights.AspNetCore 3.2.0; Microsoft.Azure.Cosmos 3.64.0; Microsoft.EntityFrameworkCore.SqlServer 11.0.0; Microsoft.EntityFrameworkCore.Tools 11.0.0; Microsoft.OpenApi 3.11.1; Newtonsoft.Json 13.0.4; Swashbuckle.AspNetCore 11.0.1.
- **Version reconciliation**: Not applicable; assessment versions are unified for the single scoped project, with no unsupported-version findings.
- **Package management**: No `Directory.Packages.props` was found; update package versions in `WebApp/WebApp.csproj`.
- **Breaking-change handling**: Migrate actual Azure.Identity, Microsoft.ApplicationInsights.AspNetCore, and Microsoft.OpenApi usages affected by the API diffs; use build feedback to locate usages because assessment was quick (`fullScan=false`). Treat reported removals that are not used as no-op findings. Other assessed packages have no source-breaking API changes; no source changes are planned for them unless validation demonstrates otherwise.
- **Excluded projects**: None.
