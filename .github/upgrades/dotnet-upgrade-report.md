# .NET 10.0 Upgrade Report

## Project target framework modifications

| Project name                                   | Old Target Framework    | New Target Framework         | Commits                   |
|:-----------------------------------------------|:-----------------------:|:----------------------------:|:-------------------------:|
| WebApp                                         |   net9.0               | net10.0                      | 6912a418                 |

## NuGet Packages

| Package Name                                   | Old Version | New Version | Commit Id                                 |
|:-----------------------------------------------|:-----------:|:-----------:|:-------------------------------------------|
| Azure.Identity                                 | 1.13.1     | 1.17.1     | a042e064                                   |
| Microsoft.ApplicationInsights.AspNetCore       | 2.22.0     | 2.23.0     | a042e064                                   |
| Microsoft.EntityFrameworkCore.SqlServer       | 9.0.0      | 10.0.2     | a042e064                                   |
| Microsoft.EntityFrameworkCore.Tools           | 9.0.0      | 10.0.2     | a042e064                                   |
| Newtonsoft.Json                                | 13.0.3     | 13.0.4     | a042e064                                   |

## All commits

| Commit ID              | Description                                |
|:-----------------------|:-------------------------------------------|
| 94fba6e7               | Commit upgrade plan                         |
| 6912a418               | Update target framework to net10.0 in WebApp.csproj |
| a042e064               | Update NuGet package versions in WebApp.csproj |

## Project feature upgrades

- No feature-specific code migrations were required by the analysis and automated upgrade. Code-level breaking changes may appear during a full build/run and should be fixed if present.

## Next steps

- Run full build and unit/integration tests.
- Verify application behavior locally and in CI.
- Address any code-level compile or runtime issues.

## Notes

- Changes applied:
  - Updated `WebApp/WebApp.csproj` target framework from `net9.0` to `net10.0`.
  - Updated NuGet package versions listed above.

- Token usage and cost: Not available in this environment.
