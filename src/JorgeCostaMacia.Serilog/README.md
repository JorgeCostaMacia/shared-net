# JorgeCostaMacia.Serilog

One-call **Serilog bootstrap** for .NET apps: reads the `Serilog` configuration section, registers Serilog as the logging provider, and enriches every log entry with the entry assembly's version.

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
using JorgeCostaMacia.Serilog;

builder.Services.AddSerilogContext(builder.Configuration);
```

It reads the `Serilog` section via `ReadFrom.Configuration(...)` and applies a common enricher baseline **in code** — `FromLogContext`, `ThreadId`, `ProcessId`, exception details, plus `Version` and `Application` properties taken from the entry assembly (your host/worker). Your `appsettings.json` only declares **sinks and levels** (`Using`, `WriteTo`, `MinimumLevel`) — don't repeat those enrichers or a `Properties:Application` there. See the XML docs on `AddSerilogContext` for ready-to-use production and development `Serilog` sections.

Bundled Serilog packages: `Settings.Configuration`, `Extensions.Hosting`, `Sinks.Console`, `Exceptions`, and the `Environment` / `Process` / `Span` / `Thread` enrichers.

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

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
