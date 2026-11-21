# 02-application-insights: Upgrade Application Insights

Update Microsoft.ApplicationInsights.AspNetCore to 3.2.0 in `WebApp.csproj`. Migrate any usages affected by the removed types, `ApplicationInsightsServiceOptions` members, and middleware extensions documented in [Microsoft.ApplicationInsights.AspNetCore.apidiff.md](../../apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md). Since this was a quick assessment, use build feedback to locate concrete usages rather than assuming every listed API is referenced.

**Done when**: The package reference matches 3.2.0, used APIs are migrated to supported equivalents (or confirmed unused), and `WebApp` builds with its telemetry configuration and behavior intact.

## Execution research

- Scope confirmed in the repository: the solution contains the single scoped `WebApp` project at `WebApp/WebApp.csproj`; it is an SDK-style web project targeting `net11.0`.
- `Microsoft.ApplicationInsights.AspNetCore` is currently `2.23.0`, directly declared in `WebApp/WebApp.csproj`. `UpgradeDotNet-get_project_dependencies` confirmed the package is defined in that project; no CPM or imported package source applies. `scenario-instructions.md` also records no `Directory.Packages.props`.
- Source inventory: only `WebApp/Program.cs` references the affected package API. It imports `Microsoft.ApplicationInsights.AspNetCore.Extensions` and calls `AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration["WebApi:ConnectionString"] })`. Repository search found no uses of the removed telemetry types or the old request/exception middleware extensions.
- Existing telemetry intent is to configure Application Insights from `WebApi:ConnectionString` during service registration. Preserve that connection-string binding and default telemetry setup; use build feedback and the 2.23.0 → 3.2.0 API diff to determine whether the options construction needs a supported replacement.
- Assessment is quick (no source incidents). The API diff flags removed telemetry helper types, 39 `ApplicationInsightsServiceOptions` members, and two middleware extensions; these are not evidence of usage by themselves. Check actual usages and only migrate concrete incompatible code.
- Execution/build pattern: update the non-CPM package reference, then targeted `dotnet build WebApp/WebApp.csproj` under the installed .NET 11 SDK; the project is modern SDK-style and has no legacy/MSBuild-only features identified. No test project was identified in the scoped project inventory.
- Decomposition verdict: **atomic**. This is one project, one package reference, one telemetry registration site, and no independent concerns or intermediate validation gate. Evaluated the scenario skill's `execution.md`; it contains the applicable breakdown rules and no separately indexed Breakdown Hints files were present.
