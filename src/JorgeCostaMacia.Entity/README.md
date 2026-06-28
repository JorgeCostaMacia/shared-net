# JorgeCostaMacia.Entity

Marker interface (`IEntity`) for **identity-based domain entities** — objects defined by a stable identity across state changes, the DDD Entity building block. **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Entity.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Entity/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Entity.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Entity/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Entity
```

## Usage

```csharp
using JorgeCostaMacia.Entity.Domain;

public class Customer : IEntity
{
    public Guid Id { get; init; }
    // identity-based: two Customers are equal when their Id is equal
}
```

An **Entity** is defined by its identity and lifecycle (not its attributes), and is mutable — unlike a Value Object.

## Requirements

One of the following SDKs: **.NET 6 / 7 / 8 / 9 / 10** *(.NET 10 recommended)*.

## About

`JorgeCostaMacia.Entity` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
