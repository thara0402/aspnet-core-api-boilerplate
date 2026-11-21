# Integrity review

## Round 1 — CHANGES SUGGESTED

1. **IR-02-001 — advisory — open**  
   **Location:** `task.md:3`  
   The API-diff link resolves to `tasks/02-application-insights/apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md`, which does not exist. The document is at the scenario-level `apidiff` directory. Change the link target to `../../apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md`.

No blocking findings. The selected 3.2.0 package is present and resolves for `net11.0` through its `net10.0` assets; its shipped API documentation retains `ApplicationInsightsServiceOptions.ConnectionString` and the options overload of `AddApplicationInsightsTelemetry`. Repository source does not use the APIs identified as removed.
