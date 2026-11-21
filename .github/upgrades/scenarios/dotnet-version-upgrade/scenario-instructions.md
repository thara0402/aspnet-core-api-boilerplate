# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: .NET 11 (net11.0)

## Source Control
- **Source Branch**: thara0402-laughing-eureka
- **Working Branch**: upgrade-dotnet-11
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options
### Strategy
- Upgrade Strategy: All-at-Once

### Compatibility
- Unsupported API Handling: Fix Inline

### Reliability
- Test Coverage: Skip

## Strategy
**Selected**: All-At-Once
**Rationale**: The solution has one SDK-style ASP.NET Core project on net10.0 with a shallow dependency graph and no incompatible package or .NET Framework migration constraints, so a single atomic upgrade is the least-overhead fit.

### Execution Constraints
- Upgrade all project files, package references, and required API fixes together in one atomic pass.
- Verify SDK and global.json compatibility for net11.0 before changing the project.
- Update the WebApp project to net11.0 and align the recommended EF Core package versions with the target.
- Resolve unsupported API and behavioral change findings within the same implementation task where required; do not defer mechanical or compatible replacements.
- Complete with a solution-wide restore and build, then run the applicable existing test suite and report any failures.
