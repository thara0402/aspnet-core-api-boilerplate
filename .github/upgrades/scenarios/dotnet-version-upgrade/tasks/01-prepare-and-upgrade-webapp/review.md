# Integrity review

## Round 1 — CHANGES NEEDED

1. **IR-001 — Blocking — Open — `WebApp/Program.cs:64`**  
   `NullLoggerFactory.Instance` discards the diagnostics AutoMapper 17 emits through the newly required `ILoggerFactory`, including its invalid/missing-license warning. AutoMapper 17 contains `LogWarning` and the production-license warning (“You do not have a valid license key…”), while the package README recommends application `IServiceCollection.AddAutoMapper(...)` registration. The scenario does not authorize suppressing this signal. Adapt the registration to use the application's real `ILoggerFactory` (either through `AddAutoMapper` or a DI factory) while retaining the `ShopProfile` configuration and required `IMapper` lifetime.

Authorized/evidence-backed scope note: the direct `Microsoft.OpenApi` 2.7.5 pin is a justified forced dependency update for the vulnerable 2.3.0 transitive minimum and is not drift. The configuration-binding generator change and the recorded no-change dispositions for the 47 behavioral notices are consistent with the task.

## Round 2 — PASS

1. **IR-001 — Blocking — Resolved — `WebApp/Program.cs:59-68`**  
   The singleton `IMapper` registration now uses a DI factory, resolves the application's real `ILoggerFactory`, and passes it to `MapperConfiguration` while retaining `ShopProfile` and singleton lifetime. AutoMapper license and runtime diagnostics are no longer suppressed.

No open or newly established findings.
