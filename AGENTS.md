# shared-net — working in this repo

Foundational, self-contained .NET packages — DDD building blocks and small utilities — each scoped to a single concern and shipped independently on NuGet under `JorgeCostaMacia.*`.

## Layout

- `src/<Package>/` — one package per folder. `test/<Package>.Tests/` — its tests, plus `<Package>.IntegrationTests/` where a suite needs a real dependency. Three do: `ValueObject.EfConverter` (Testcontainers Postgres for the converter mapping), `Quartz` (Postgres for the store configuration — Quartz 4 validates the schema on start, so nothing about it can be asserted from a fake) and `Quartz.Serilog` (a live in-memory scheduler, no container). `assets/` — icons + social preview.
- **3-tier `Directory.Build.props`**: **root** (repo identity — Authors / Company / Copyright / Repository — + the single lockstep `VersionPrefix`; TFM `net10.0`; ImplicitUsings, Nullable, AnalysisLevel, EnforceCodeStyleInBuild) → **`src/`** (package-output: icon / readme / license, SourceLink, symbols, `GenerateDocumentationFile`, pack of LICENSE/COPYRIGHT/icon/README) → **`test/`** (test settings). Each `src` csproj declares **only** `Description` / `PackageTags`; everything else — the single `VersionPrefix`, package metadata, and the LICENSE/COPYRIGHT/icon/README packing — is inherited from the props (don't restate it).

## Targets & stack

- TFM: **`net10.0`**, single-target. net6/7/8/9 were dropped as they reached EOL — net9 in May 2026, net8 in November 2026 — and the last multi-targeting release is **6.0.6**, which stays on nuget.org for anyone pinned to a down-level runtime. Being single-target is what removes the per-TFM `#if NET9_0_OR_GREATER` branches and the conditional EF Core block in `Directory.Packages.props`: there is now **one** version of every package. **Do not reintroduce a per-TFM conditional** — if a dependency needs one, the answer is to move the whole repo, not to split it.
- Quartz: **4.0.1**, which net10-only unblocked (Quartz 4 ships no net8/net9 asset, so the multi-target could not reference it at all — `NU1202`). Five of its breaking changes bite here: listener members return **`ValueTask`**, `ITriggerListener.TriggerMisfired` takes an **`IScheduler`**, `IJob.Execute` takes a **`CancellationToken`** and returns `ValueTask`, `IJobExecutionContext` lost `Get`/`Put` — the per-firing bag is now `MergedJobDataMap` (a copy, persisted nowhere), which is what `JobTrace` uses — and `StdSchedulerFactory` is gone. **The listener ones fail silently**: every `IJobListener`/`ITriggerListener` member has a default implementation, so a `Task`-returning or short-signature member compiles, the default runs and the listener is never called. Quartz 4 guards it at `AddJobListener` and the integration tests are what catch it — never register a listener without one. The `StdSchedulerFactory` removal takes configuration by property strings and reflection with it: a test or console app now builds a scheduler with `QuartzSchedulerBuilder.Create(q => q.ConfigureScheduler(...).UseDefaultThreadPool(n).UseInMemoryStore()).BuildScheduler()`.
- Tests: **xUnit v4 (the `xunit.v3` package, 4.x) on Microsoft.Testing.Platform v2 (MTP)** — test projects are `OutputType=Exe`, and `dotnet test` runs MTP because the root **`global.json`** opts in (`"test": { "runner": "Microsoft.Testing.Platform" }`). MTP v2 dropped the VSTest bridge, so `TestingPlatformDotnetTestSupport` is gone, the `xunit.runner.visualstudio` adapter is **not referenced** (it is the VSTest adapter — the suite runs identically without it), and running the tests needs the **.NET 10 SDK**. Not MSTest, not VSTest.
- Source is **UTF-8 without BOM** (`.editorconfig` `charset = utf-8`). camelCase locals, PascalCase types, I-prefixed interfaces. Copyright year stays **2023** (deliberate — don't bump).
- **Explicit types everywhere — spell the type out.** Never `var`, never target-typed `new()`, never collection expressions `[]`: write `new Foo(...)`, `new byte[] { ... }`, `new List<T> { ... }`, `Array.Empty<T>()`. The `.editorconfig` sets all three to explicit, but only `var` is analyzer-enforced — `new()`/`[]` can't be flagged (the analyzer never reports the implicit form), so `develop.yml` guards them with a **grep step** that fails the build if either reappears. Do not introduce `new()`/`[]` when editing — CI, not just review, will reject it.

## The value-object factory surface

Every value object carries **four** public static factories, and a contract test
(`ValueObjectCreationSurfaceTests`) scans the assembly by reflection and fails if one is missing:

| verb | input | returns | semantics |
|---|---|---|---|
| `From(T)` | a value | **non-null** | converts through `Convert`, **unvalidated** |
| `Create(T)` | a value | **non-null** | `From` + `Validate()` — nothing invalid escapes |
| `FromOrNull(T?)` | a value or nothing | nullable | absence propagates, still unvalidated |
| `CreateOrNull(T?)` | a value or nothing | nullable | absence short-circuits; a supplied value is validated |

Two rules keep the pair honest:

- **The `OrNull` pair is about absence, not failure.** It answers "the field did not come, what do I
  build?" — never "this might be invalid, don't throw at me". That is why there is no `TryFrom`:
  `From` only converts and cannot fail, so there is nothing to try. Nor a `FromOrDefault`: an
  out-of-range value is a caller mistake, and coercing it would hide the mistake *and* give two
  callers different behaviour for the same rule.
- **`CreateOrNull` branches on what `FromOrNull` returned**, not on whether the input was null, and
  validates with `vo?.Validate()`. Branching on the input would hard-code "non-null in, non-null
  out" and would break the day a `From` maps a sentinel to absence.

The ten bases that derive from another base (`EmailValueObject : StringValueObject`,
`PageSizeValueObject : IntValueObject`, `DateTimeUtcValueObject : DateTimeValueObject`, …) declare all
four with **`static new`**, because a static factory returning the derived type cannot be inherited.
The compiler proves each one is needed: `CS0109` ("`new` not required") would appear otherwise, and
the build carries zero warnings.

## Inter-package dependencies

Packages reference each other via **`ProjectReference`** (e.g. ValueObject → Exception, Aggregate → DomainEvent). `dotnet pack` turns each `ProjectReference` into a NuGet `<dependency>` at the sibling's version, so the dependency graph still ships in the nuspec — but you build against local source and **release everything together** (no phased, tier-by-tier publishing). Don't reintroduce `PackageReference` between these packages.

## Extend the framework's options, never replace its call

A package that carries a policy publishes it as an **extension on the type the framework already hands the host** — `LoggerConfiguration.WithDefaults()`, `IQuartzBuilder.WithPostgresDefaults()` — and never as an `Add…` method that wraps the framework's own. The host keeps writing `AddSerilog(…)` or `AddQuartz(…)`, so what it composes stays readable in its `ProgramBuilder`; only the policy moves here. `http-net` follows the same rule with its `ApiVersioningOptions` / `ProblemDetailsOptions` extensions, and its doc comments say so.

Two facades were removed in 7.0.0 for breaking it: `AddSerilogContext`, which replaced `AddSerilog` and which no host in the family had adopted — each had hand-rolled the enrichers instead — and `AddValueObjectContext`, which registered validators nothing ever resolved, since each assembles itself through its static `Create()`. **Don't add another.** A policy belongs in an extension when the framework cannot read it from configuration; when it can — Serilog's sinks, levels and overrides — leave it in `appsettings`, where it can differ per app and per environment.

## Dependencies — Central Package Management

Third-party package versions are centralized in **`Directory.Packages.props`** (repo root, `ManagePackageVersionsCentrally=true`): add or bump them **there** as `<PackageVersion>`, and reference packages in csproj **without** a `Version`. (Inter-package deps are `ProjectReference`, not packages — see above — so CPM doesn't manage them.)

## CI / publishing

`.github/workflows/main.yml`: push to `main` → build → test → `dotnet pack shared-net.slnx` → `dotnet nuget push *.nupkg --skip-duplicate`. **The (central) `VersionPrefix` is the publish gate** — only new versions publish, existing ones are skipped, so pushing `main` after a version bump ships the whole set. `develop.yml` builds/tests on develop + PRs (no publish).

## Branching & releases — GitFlow

Use the **`gitflow` skill** for any branch/release work — never improvise.

- Feature/bugfix → `feature/`|`bugfix/<name>-<ts>` from develop → finish `--no-ff` into develop.
- Release → `release/<version>` from develop → bump the **single** `VersionPrefix` in the **root** `Directory.Build.props` (the one holding the solution identity — all packages version in lockstep) → Release Finish (merge develop+main, annotated tag `v<version>`, atomic push). One bump versions everything; the `ProjectReference` cross-deps follow automatically.
- Use git's **default merge message** (`--no-ff --no-edit`, never `-m`).
- Branch prefixes only: `feature` / `bugfix` / `release` / `hotfix`.

## Git etiquette

- Commit under **your own identity** — don't hardcode anyone's name/email.
- Keep history clean — **no** `Co-Authored-By` / AI-assistant trailers in commits or messages.
- Merges use git's **default** message (see *Branching & releases* above).

## Relevant skills

Skills that apply to this repo — let them trigger, or invoke explicitly. `gitflow`, `solid`, `clean-architecture`, `ddd`, `testing`, `logging-net` and `validation-net` are from `jorgecostamacia-agent-skills`; the rest from `dotnet-agent-skills` (the `dotnet/skills` marketplace).

- **`gitflow`** — all branch/release work (see *Branching & releases* above).
- **`solid`** — SOLID-principles design review; apply when shaping or reviewing the public surface of the DDD building blocks (ValueObject, Aggregate, Exception…).
- **`clean-architecture`** — layers and the inward dependency rule; here mainly the Domain-stays-pure discipline behind the package namespaces.
- **`ddd`** — tactical DDD, canon-anchored: aggregates, value objects, factories & hydration, validation principles, domain events, domain errors. This repo IS those building blocks — the skill is its conceptual spec.
- **`testing`** — testing principles: done-means-tested, one test file per unit, names as specification, classicist doubles, rule coverage.
- **`logging-net`** — the logging style for every log statement: fixed low-cardinality messages as grouping keys (no interpolation, no placeholders), all variable data via `LogContext` (a `PushProperties` helper per class), correlation ids in every scope. The `JorgeCostaMacia.Serilog` and `Quartz.Serilog` packages implement it — the first as a `LoggerConfiguration` extension (`WithDefaults()`) called inside the host's own `AddSerilog`, deliberately not an `Add…` facade, so the composition stays visible in `ProgramBuilder`.
- **`validation-net`** — **the spec this library implements**: the creation surface (ctor hydrates · `From` converts · `Create` validates · the `OrNull` pair propagates absence — see *The value-object factory surface* above), per-call validators assembled via static `Create()` chains, family exceptions with fixed codes, the factory-vs-DI rule. Read it before touching ValueObject/Aggregate creation or validators.
- **`dotnet`** — C# language server + general .NET development.
- **`dotnet-msbuild`** — `Directory.Build.props`, project-file quality/review, Central Package Management, build perf, modernization (msbuild-antipatterns, directory-build-organization, convert-to-cpm…).
- **`dotnet-nuget`** — dependency management and package modernization.
- **`dotnet-test`** — running, generating and analyzing tests; coverage; testability.
- **`dotnet-test-migration`** — framework/platform migrations (the xUnit.v3 / MTP setup here; future migrations in bus-net / http-net).
- **`dotnet-upgrade`** — migrating across target-framework versions.
- **`dotnet-data`** — EF Core / data access (the `GuidMySqlConverter` and the value-object EF-mapping guidance).

Not relevant to this foundation library (skip): `dotnet-ai`, `dotnet-maui`, `dotnet-aspnetcore` (that's for **http-net**), `dotnet-blazor`, `dotnet-template-engine`, `dotnet11`, `dotnet-diag`, `dotnet-advanced`.

## Build & test

```
dotnet format shared-net.slnx                  # apply .editorconfig (using order, whitespace) — run before committing
dotnet build  shared-net.slnx -c Release
dotnet test   shared-net.slnx -c Release       # MTP v2 via global.json (needs the .NET 10 SDK + Docker); --logger is VSTest-only (MTP0001)
dotnet pack   shared-net.slnx -c Release        # packs all packable; tests are IsPackable=false
```

Two suites need **Docker running locally** — `ValueObject.EfConverter.IntegrationTests` and `Quartz.IntegrationTests` each start a Testcontainers Postgres, and without a daemon `dotnet test` fails there rather than skipping. `Quartz.Serilog.IntegrationTests` needs nothing external: it drives a real but in-memory Quartz scheduler.

One runtime requirement the packages cannot declare: `WithPostgresDefaults` makes Quartz resolve its ADO provider **by name at runtime**, so a host calling it must reference `Npgsql` itself. `JorgeCostaMacia.Quartz` deliberately does not depend on it — a consumer that only wants `JobTrace` should not pull in a database driver — and a host that forgets fails at startup with `ArgumentException: Error while reading metadata information for provider 'Npgsql'`.

Run **`dotnet format` before committing** — it applies the `.editorconfig` (using ordering, whitespace), the CLI equivalent of Visual Studio's *Code Cleanup*, so generated code doesn't drift from what the IDE would produce. It is deliberately **not** a CI step: `--verify-no-changes` checks the whole solution, so a file nobody touched can fail someone else's push — and on a Linux runner it always would, since `.gitattributes` stores LF while `.editorconfig` requires CRLF. Whitespace and using order are the author's job before committing; anything that is an analyzer diagnostic already fails the build through `EnforceCodeStyleInBuild`.
