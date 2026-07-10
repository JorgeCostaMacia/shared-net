# JorgeCostaMacia.Quartz.Serilog

**Serilog observability for Quartz**: job and trigger log listeners that emit **fixed, low-cardinality messages** (`JobToBeExecuted`, `JobWasExecuted`, `TriggerFired`, `TriggerMisfired`…) and push everything variable — scheduler, job, trigger, data, times and the trace identifiers — through the Serilog log context. Cheap to index (Loki/Grafana-friendly), structural to search.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Quartz.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Quartz.Serilog/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Quartz.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Quartz.Serilog/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Quartz.Serilog
```

## Usage

```csharp
using JorgeCostaMacia.Quartz.Serilog;
using JorgeCostaMacia.Quartz.Serilog.Infrastructure;

services
    .AddSerilogContext()   // registers both listeners as singletons
    .AddQuartz(q =>
    {
        // ... your scheduler/store configuration ...
        q.AddJobListener<JobsLoggerListener>();
        q.AddTriggerListener<TriggerLoggerListener>();
    });
```

- **`JobsLoggerListener`** — `JobToBeExecuted` (Information), `JobExecutionVetoed` (Warning), `JobWasExecuted` (Information, or **Error with the root cause attached** when the job threw).
- **`TriggerLoggerListener`** — `TriggerFired` / `TriggerComplete` (Information, with the scheduler's resulting instruction), `VetoJobExecution` (Information, never vetoes), `TriggerMisfired` (**Error** — the trigger missed its scheduled time).
- Every event carries `AggregateId`/`CorrelationId` from [`JobTrace`](https://www.nuget.org/packages/JorgeCostaMacia.Quartz/) — idempotent get-or-create on the execution context, so your own listeners (e.g. event publishers) read the same pair with `JobTrace.GetOrCreate(context)`, in any order.

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

Depends on [JorgeCostaMacia.Quartz](https://www.nuget.org/packages/JorgeCostaMacia.Quartz/), [Quartz](https://www.nuget.org/packages/Quartz/) and [Serilog](https://www.nuget.org/packages/Serilog/).

## About

`JorgeCostaMacia.Quartz.Serilog` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
