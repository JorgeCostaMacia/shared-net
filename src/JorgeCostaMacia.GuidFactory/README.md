# JorgeCostaMacia.GuidFactory

Factory for creating time-ordered GUIDs (**UUIDv7**), with automatic fallback to `Guid.NewGuid()` (v4) on older runtimes. **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.GuidFactory.svg)](https://www.nuget.org/packages/JorgeCostaMacia.GuidFactory/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.GuidFactory.svg)](https://www.nuget.org/packages/JorgeCostaMacia.GuidFactory/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.GuidFactory
```

## Usage

```csharp
using JorgeCostaMacia.GuidFactory.Domain;

Guid id = GuidFactory.Create();
```

On **.NET 9+** it returns a time-ordered **UUIDv7** (`Guid.CreateVersion7()`) — better database index locality and natural sortability. On earlier runtimes it falls back to a standard **UUIDv4** (`Guid.NewGuid()`).

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

## About

`JorgeCostaMacia.GuidFactory` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
