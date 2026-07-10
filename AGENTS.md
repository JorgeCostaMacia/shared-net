# shared-net — working in this repo

Foundational, self-contained .NET packages — DDD building blocks and small utilities — each scoped to a single concern and shipped independently on NuGet under `JorgeCostaMacia.*`.

## Layout

- `src/<Package>/` — one package per folder. `test/<Package>.Tests/` — its tests. `assets/` — icons + social preview.
- **3-tier `Directory.Build.props`**: **root** (repo identity — Authors / Company / Copyright / Repository — + the single lockstep `VersionPrefix`; TFMs `net8.0;net9.0;net10.0`; ImplicitUsings, Nullable, AnalysisLevel, EnforceCodeStyleInBuild) → **`src/`** (package-output: icon / readme / license, SourceLink, symbols, `GenerateDocumentationFile`, pack of LICENSE/COPYRIGHT/icon/README) → **`test/`** (test settings). Each `src` csproj declares **only** `Description` / `PackageTags`; everything else — the single `VersionPrefix`, package metadata, and the LICENSE/COPYRIGHT/icon/README packing — is inherited from the props (don't restate it).

## Targets & stack

- TFMs: **`net8.0;net9.0;net10.0`** (net6/7 dropped — EOL). No per-TFM conditionals except the `#if NET9_0_OR_GREATER` GUID branch in Exception/ValueObject.
- Tests: **xUnit.v3 on Microsoft.Testing.Platform (MTP)** — test projects are `OutputType=Exe` + `TestingPlatformDotnetTestSupport=true`. Not MSTest, not VSTest.
- Source is **UTF-8 without BOM** (`.editorconfig` `charset = utf-8`). camelCase locals, PascalCase types, I-prefixed interfaces. Copyright year stays **2023** (deliberate — don't bump).

## Inter-package dependencies

Packages reference each other via **`ProjectReference`** (e.g. ValueObject → Exception → GuidFactory, Aggregate → DomainEvent). `dotnet pack` turns each `ProjectReference` into a NuGet `<dependency>` at the sibling's version, so the dependency graph still ships in the nuspec — but you build against local source and **release everything together** (no phased, tier-by-tier publishing). Don't reintroduce `PackageReference` between these packages.

## Dependencies — Central Package Management

Third-party package versions are centralized in **`Directory.Packages.props`** (repo root, `ManagePackageVersionsCentrally=true`): add or bump them **there** as `<PackageVersion>`, and reference packages in csproj **without** a `Version`. (Inter-package deps are `ProjectReference`, not packages — see above — so CPM doesn't manage them.)

## CI / publishing

`.github/workflows/main.yml`: push to `main` → build → test → `dotnet pack shared-net.slnx` → `dotnet nuget push *.nupkg --skip-duplicate`. **The (central) `VersionPrefix` is the publish gate** — only new versions publish, existing ones are skipped, so pushing `main` after a version bump ships the whole set. `develop.yml` builds/tests on develop + PRs (no publish).

## Branching & releases — GitFlow

Use the **`gitflow` skill** for any branch/release work — never improvise.

- Feature/bugfix → `feature/`|`bugfix/<name>-<ts>` from develop → finish `--no-ff` into develop.
- Release → `release/<version>` from develop → bump the **single** `VersionPrefix` in `src/Directory.Build.props` (all packages version in lockstep) → Release Finish (merge develop+main, annotated tag `v<version>`, atomic push). One bump versions everything; the `ProjectReference` cross-deps follow automatically.
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
- **`logging-net`** — the logging style for every log statement: fixed low-cardinality messages as grouping keys (no interpolation, no placeholders), all variable data via `LogContext` (a `PushProperties` helper per class), correlation ids in every scope. The `JorgeCostaMacia.Serilog` and `Quartz.Serilog` packages implement it.
- **`validation-net`** — **the spec this library implements**: the three-verb surface (ctor hydrates · `From` converts · `Create` validates), per-call validators assembled via static `Create()` chains, family exceptions with fixed codes, the factory-vs-DI rule. Read it before touching ValueObject/Aggregate creation or validators (v4.0.0 implements it).
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
dotnet test   shared-net.slnx -c Release       # MTP prints a per-assembly summary; --logger is VSTest-only (MTP0001)
dotnet pack   shared-net.slnx -c Release        # packs all packable; tests are IsPackable=false
```

Run **`dotnet format` before committing** — it applies the `.editorconfig` (using ordering, whitespace), the CLI equivalent of Visual Studio's *Code Cleanup*, so generated code doesn't drift from what the IDE would produce.
