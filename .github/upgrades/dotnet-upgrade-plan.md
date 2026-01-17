# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade `WebApp/WebApp.csproj`

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| (none)                                         | No projects excluded        |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                   | Current Version | New Version | Description                                   |
|:-----------------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| Azure.Identity                                 |   1.13.1        |  1.17.1     | Replace deprecated dependency on older MSAL .NET; recommended update for compatibility with .NET 10.0 |
| Microsoft.ApplicationInsights.AspNetCore       |   2.22.0        |  2.23.0     | Version is deprecated; update to latest supported version                                     |
| Microsoft.EntityFrameworkCore.SqlServer       |   9.0.0         | 10.0.2      | Update EF Core provider to match .NET 10.0                                                      |
| Microsoft.EntityFrameworkCore.Tools           |   9.0.0         | 10.0.2      | Update EF Core tools to match .NET 10.0                                                         |
| Newtonsoft.Json                                |   13.0.3        | 13.0.4     | Patch update available; recommended for stability and compatibility                             |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### WebApp modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`.

NuGet packages changes:
  - `Azure.Identity` should be updated from `1.13.1` to `1.17.1` (*deprecated dependency fix*).
  - `Microsoft.ApplicationInsights.AspNetCore` should be updated from `2.22.0` to `2.23.0` (*deprecated version*).
  - `Microsoft.EntityFrameworkCore.SqlServer` should be updated from `9.0.0` to `10.0.2` (*match .NET 10.0, package upgrade*).
  - `Microsoft.EntityFrameworkCore.Tools` should be updated from `9.0.0` to `10.0.2` (*match .NET 10.0, tools upgrade*).
  - `Newtonsoft.Json` should be updated from `13.0.3` to `13.0.4` (*patch update*).

Feature upgrades:
  - No additional feature-specific changes were detected by analysis. Code-level breaking changes may need to be addressed during upgrade if compile errors appear.

Other changes:
  - None identified in analysis.
