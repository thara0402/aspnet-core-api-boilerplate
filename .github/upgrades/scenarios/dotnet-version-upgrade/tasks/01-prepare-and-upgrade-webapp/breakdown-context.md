# Breakdown context — 01-prepare-and-upgrade-webapp

## Research snapshot
- Scope is the single SDK-style ASP.NET Core project `WebApp/WebApp.csproj` in `aspnet-core-api-boilerplate.sln`, current `net10.0` → target `net11.0`.
- No project references/dependents, no test project, no central package management, and no global.json.
- SDK 11.0.100/MSBuild 18.12.2 is installed and the upgrade SDK validator reports compatible.
- Three version-only package updates are recommended (AutoMapper 17.0.0, EFCore.SqlServer 11.0.0, EFCore.Tools 11.0.0); preserve the other nine compatible packages.
- One required API incident in `Program.cs` can be addressed by enabling the documented configuration-binding source generator while preserving the existing Configure binding call. The other 47 API incidents are behavioral notices reviewed in `task.md`; no speculative source changes are indicated.
- The task and scenario instructions explicitly request one atomic upgrade pass (All-at-Once strategy); solution validation is a single final gate.

## Hint evaluation
- `breakdown-hints/common.md`: multi-project dependency ordering does not apply (one project); >5 package replacement batch does not apply (no replacements; only three version bumps).
- Flavor index: this is an ASP.NET Core project already on .NET, not a .NET Framework web migration. It has no test dependents. No Framework-migration, framework-web-migration, or test hint files apply.
- A repository scan found no existing `// STUB:` markers in WebApp. This scan was completed after the first project-file edit; it found no stubs requiring resolution.

## Verdict to TaskBreaker
One coherent single-project upgrade appears atomic under the selected scenario strategy, but the executor's core rule requires TaskBreaker escalation when a task covers an entire project. Evaluate whether this scenario's tightly coupled target/package/API changes should remain one atomic task.

## Breakdown Decisions

### task: 01-prepare-and-upgrade-webapp
- **Decision**: Atomic; no subtasks registered.
- **Reason**: The selected All-at-Once strategy expressly requires TFM, package, and required API changes to be completed together. Scope is one SDK-style project, there is no project dependency ordering, only three package version bumps, no test project, and a single solution restore/build validation gate.
- **Hints**: `multi-project-dependency-ordering` does not apply (one project); `large-package-replacement-batch` does not apply (no replacements, three version bumps). No skill-provided breakdown hints were present.
