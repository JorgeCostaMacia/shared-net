# JorgeCostaMacia.Serilog

The **family Serilog baseline** as a `LoggerConfiguration` extension: one call inside your own `AddSerilog` attaches the enrichers every app shares, plus the entry assembly's version and name.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Serilog/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Serilog/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Serilog
```

## Usage

```csharp
using JorgeCostaMacia.Serilog.Infrastructure;

builder.Services.AddSerilog((_, config) => config
    .ReadFrom.Configuration(builder.Configuration)
    .WithDefaults());
```

`WithDefaults()` is an extension on `LoggerConfiguration`, **not** an `Add…` facade: your `AddSerilog` call stays in your `Program`, so what the host composes is visible where it happens. It attaches `FromLogContext`, `ThreadId`, `ProcessId` and exception details, plus `Version` and `Application` read from the entry assembly — the two a configuration file cannot express.

Your `Serilog` section then declares only what varies per app and per environment:

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": {
      "Default": "Error",
      "Override": { "Microsoft.Hosting.Lifetime": "Information" }
    },
    "WriteTo": [
      { "Name": "Console", "Args": { "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog" } }
    ]
  }
}
```

No `Enrich` array, no enricher assemblies in `Using`, and **no `Properties:Application`** — Serilog adds properties *if absent*, so a value there would win over the assembly name and you would never see it.

Bundled Serilog packages: `Exceptions` and the `Process` / `Thread` enrichers — the ones the baseline needs. Your sinks, and `Serilog.Settings.Configuration` for the section above, stay with the host that chooses them.

## Requirements

The **.NET 10** SDK.

## About

`JorgeCostaMacia.Serilog` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
