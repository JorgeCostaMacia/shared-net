# shared-net — working in this repo

Foundational, self-contained .NET packages — DDD building blocks and small utilities — each scoped to a single concern and shipped independently on NuGet under `JorgeCostaMacia.*`.

## Layout

- `src/<Package>/` — one package per folder. `test/<Package>.Tests/` — its tests. `assets/` — icons + social preview.
- **3-tier `Directory.Build.props`**: **root** (common: ImplicitUsings, Nullable, AnalysisLevel, EnforceCodeStyleInBuild) → **`src/`** (package metadata, TFMs, SourceLink, symbols, `GenerateDocumentationFile`, pack of LICENSE/COPYRIGHT/icon) → **`test/`** (test settings). Each `src` csproj declares **only** `VersionPrefix` / `Description` / `PackageTags` + its `README.md` None item; everything else is inherited — don't restate inherited settings.

## Targets & stack

- TFMs: **`net8.0;net9.0;net10.0`** (net6/7 dropped — EOL). No per-TFM conditionals except the `#if NET9_0_OR_GREATER` GUID branch in Exception/ValueObject.
- Tests: **xUnit.v3 on Microsoft.Testing.Platform (MTP)** — test projects are `OutputType=Exe` + `TestingPlatformDotnetTestSupport=true`. Not MSTest, not VSTest.
- Source is **UTF-8 without BOM** (`.editorconfig` `charset = utf-8`). camelCase locals, PascalCase types, I-prefixed interfaces. Copyright year stays **2023** (deliberate — don't bump).

## Inter-package dependencies — releases are PHASED

Packages reference each other via **`PackageReference` to PUBLISHED versions** (zero `ProjectReference`). A dependent can only build against a version already on NuGet, so a change spanning tiers must publish **in order**: **tier0** (GuidFactory, DomainEvent, Entity, ExpressionConverter, GuidMySqlConverter, Serilog) → wait for NuGet to index → **tier1** (Exception→GuidFactory, Aggregate→DomainEvent: bump refs + publish) → **ValueObject** (→Exception). **Never bump a dependent to reference a version that isn't published yet.**

## CI / publishing

`.github/workflows/main.yml`: push to `main` → build → test → `dotnet pack shared-net.slnx` → `dotnet nuget push *.nupkg --skip-duplicate`. **The csproj `VersionPrefix` is the publish gate** — only new versions publish, existing ones are skipped, so pushing `main` ships whatever has a bumped version. `dev.yml` builds/tests on develop + PRs (no publish).

## Branching & releases — GitFlow

Use the **`gitflow` skill** for any branch/release work — never improvise.

- Feature/bugfix → `feature/`|`bugfix/<name>-<ts>` from develop → finish `--no-ff` into develop.
- Release → `release/<version>` from develop → bump `VersionPrefix`(s) **on the branch** → Release Finish (merge develop+main, annotated tag `v<version>`, atomic push). For **phased rollouts** (the PackageReference constraint above), flag it before deviating from a single release branch.
- Use git's **default merge message** (`--no-ff --no-edit`, never `-m`).
- Branch prefixes only: `feature` / `bugfix` / `release` / `hotfix`.

## Git etiquette

- Commit under **your own identity** — don't hardcode anyone's name/email.
- Keep history clean — **no** `Co-Authored-By` / AI-assistant trailers in commits or messages.
- Merges use git's **default** message (see *Branching & releases* above).

## Relevant skills (installed)

These plugins/skills are installed and apply when working here — let them trigger, or invoke explicitly:

- **`gitflow`** — all branch/release work (see *Branching & releases* above).
- **`dotnet-msbuild`** (msbuild, `msbuild-modernization`, `msbuild-antipatterns`, `directory-build-organization`, `convert-to-cpm`) — `Directory.Build.props`, project-file review/quality, Central Package Management, build perf.
- **`dotnet-test-migration`** (`migrate-vstest-to-mtp`, `migrate-xunit-to-xunit-v3`) — the xUnit.v3 / MTP setup, and future test migrations in bus-net / http-net.
- **`dotnet-nuget`** — dependency management and package modernization.
- **`dotnet-upgrade`** (`migrate-dotnet8-to-dotnet9`, …) — when bumping target frameworks.

## Build & test

```
dotnet build shared-net.slnx -c Release
dotnet test  shared-net.slnx -c Release --logger "console;verbosity=minimal"
dotnet pack  shared-net.slnx -c Release        # packs all packable; tests + meta placeholder are IsPackable=false
```
