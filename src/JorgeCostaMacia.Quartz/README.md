# JorgeCostaMacia.Quartz

Quartz job-execution **trace correlation**: `JobTrace` get-or-creates the `AggregateId`/`CorrelationId` pair on the execution context, so every observer of the same execution — log listeners, event publishers, the job itself — shares the same identifiers **regardless of who runs first**.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Quartz.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Quartz/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Quartz.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Quartz/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Quartz
```

## Usage

```csharp
using JorgeCostaMacia.Quartz.Domain;

// In ANY observer of the execution (a listener, the job itself):
JobTrace trace = JobTrace.GetOrCreate(context);   // reads the pair, minting + storing it if absent

await bus.Publish(new JobCompletedEvent(..., trace.AggregateId, trace.CorrelationId));
```

`GetOrCreate` is **idempotent**: the first caller mints the pair (time-ordered UUIDv7s) and puts it on the firing's own `MergedJobDataMap`; every later caller reads the same values. There is no registration-order contract between listeners — the keys and the get-or-create logic live once, here, instead of being re-implemented per listener per service.

For observers **without** an execution context (e.g. a trigger misfire), `JobTrace.Create()` mints a fresh, unshared pair.

## Requirements

The **.NET 10** SDK.

Depends on [Quartz](https://www.nuget.org/packages/Quartz/).

## About

`JorgeCostaMacia.Quartz` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
