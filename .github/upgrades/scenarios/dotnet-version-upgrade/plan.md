# .NET 11 Upgrade Plan

## Overview

**Target**: Upgrade WebApp from net10.0 to net11.0.
**Scope**: One small SDK-style ASP.NET Core project with 12 NuGet packages and a single binary-incompatible API finding.

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: The solution contains one modern SDK-style project with a clear dependency structure, no framework migration mechanics, and straightforward TFM, package, and API changes.

**Project list**: WebApp (`WebApp/WebApp.csproj`), the only project in the solution.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | All-at-Once | A single-project modern upgrade is best handled as an atomic TFM and dependency update. |
| Unsupported API Handling | Fix Inline | Resolve the small set of API findings within the implementation task rather than introducing stubs or deferred work. |
| Test Coverage | Skip | Test baseline generation was explicitly not selected; proceed with the repository's applicable existing validation. |

## Tasks

### 01-prepare-and-upgrade-webapp: Prepare and upgrade the WebApp project

Verify the .NET 11 SDK/toolchain and any global.json configuration, then upgrade the solution's single SDK-style ASP.NET Core project in one atomic pass. For `WebApp/WebApp.csproj`, retarget from net10.0 to net11.0 and update the 3 recommended NuGet packages: AutoMapper, Microsoft.EntityFrameworkCore.SqlServer, and Microsoft.EntityFrameworkCore.Tools. Keep package updates and API/source fixes within this task.

The assessment identifies 48+ affected locations, with 47 behavioral-change API notices and 1 binary-incompatible API finding. Review the assessment's API details and source incidents rather than converting behavioral notices into speculative fixes. Resolve the binary-incompatible API inline and apply only evidence-backed replacements for behavioral API references. Preserve application behavior and existing package compatibility for the 9 packages that require no upgrade.

**Done when**: The SDK/toolchain is net11-compatible, `WebApp/WebApp.csproj` targets net11.0, all recommended package updates are applied, the binary-incompatible API issue is resolved inline, the solution restores and builds with 0 errors, and the applicable existing tests pass.
