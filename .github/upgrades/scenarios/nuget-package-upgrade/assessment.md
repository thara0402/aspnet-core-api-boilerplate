# NuGet package upgrade assessment

_Mode: **quick assessment** — package API diffs only; no per-project source scan was run._

## Recommended versions

- **AutoMapper**: **17.0.0** (unified across 1 project(s)).
- **Azure.Data.Tables**: **12.13.0** (unified across 1 project(s)).
- **Azure.Extensions.AspNetCore.Configuration.Secrets**: **1.5.2** (unified across 1 project(s)).
- **Azure.Identity**: **1.21.0** (unified across 1 project(s)).
- **Azure.Storage.Blobs**: **12.30.2** (unified across 1 project(s)).
- **Azure.Storage.Queues**: **12.28.2** (unified across 1 project(s)).
- **Microsoft.ApplicationInsights.AspNetCore**: **3.2.0** (unified across 1 project(s)).
- **Microsoft.Azure.Cosmos**: **3.64.0** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.SqlServer**: **11.0.0** (unified across 1 project(s)).
- **Microsoft.EntityFrameworkCore.Tools**: **11.0.0** (unified across 1 project(s)).
- **Microsoft.OpenApi**: **3.11.1** (unified across 1 project(s)).
- **Newtonsoft.Json**: **13.0.4** (unified across 1 project(s)).
- **Swashbuckle.AspNetCore**: **11.0.1** (unified across 1 project(s)).

## Public API changes

> **Types moved (namespace changed) are not removals.** A moved type keeps its name and members;
> the fix is a `using`-directive change, not a rewrite. Do not treat a moved type as deleted.

- **Azure.Data.Tables**: no source-breaking public API changes detected — see [`apidiff/Azure.Data.Tables.apidiff.md`](apidiff/Azure.Data.Tables.apidiff.md).
- **Azure.Extensions.AspNetCore.Configuration.Secrets**: no source-breaking public API changes detected — see [`apidiff/Azure.Extensions.AspNetCore.Configuration.Secrets.apidiff.md`](apidiff/Azure.Extensions.AspNetCore.Configuration.Secrets.apidiff.md).
- **Azure.Identity**: 55 type(s) removed — see [`apidiff/Azure.Identity.apidiff.md`](apidiff/Azure.Identity.apidiff.md).
- **Azure.Storage.Blobs**: no source-breaking public API changes detected — see [`apidiff/Azure.Storage.Blobs.apidiff.md`](apidiff/Azure.Storage.Blobs.apidiff.md).
- **Azure.Storage.Queues**: no source-breaking public API changes detected — see [`apidiff/Azure.Storage.Queues.apidiff.md`](apidiff/Azure.Storage.Queues.apidiff.md).
- **Microsoft.ApplicationInsights.AspNetCore**: 22 type(s) removed, 39 member(s) removed — see [`apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md`](apidiff/Microsoft.ApplicationInsights.AspNetCore.apidiff.md).
- **Microsoft.Azure.Cosmos**: no source-breaking public API changes detected — see [`apidiff/Microsoft.Azure.Cosmos.apidiff.md`](apidiff/Microsoft.Azure.Cosmos.apidiff.md).
- **Microsoft.OpenApi**: 10 member(s) removed, 24 signature(s) changed — see [`apidiff/Microsoft.OpenApi.apidiff.md`](apidiff/Microsoft.OpenApi.apidiff.md).
- **Swashbuckle.AspNetCore**: no source-breaking public API changes detected — see [`apidiff/Swashbuckle.AspNetCore.apidiff.md`](apidiff/Swashbuckle.AspNetCore.apidiff.md).

## Breaking-change findings

- Version divergence findings (Pkg.0003): 0
- Requested-version-unsupported findings (Pkg.0002): 0

- Quick mode does not scan source, so there are no per-line `PkgApi` usage findings. Review the
  per-package API diffs above and rely on build errors during execution to pinpoint affected code.
- A full code scan can locate the exact source location of every breaking-change usage across the repo.
  It is opt-in and slower — re-run the assessment with `fullScan=true` only if the user requests it.

## Next steps

1. Proceed to planning to triage the API changes above and plan the code fixes (if any).

