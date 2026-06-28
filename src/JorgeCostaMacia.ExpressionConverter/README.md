# JorgeCostaMacia.ExpressionConverter

Convert simple LINQ predicate expressions to and from `Dictionary<string, string>` — handy for serializing filters (query strings, message payloads, storage). **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.ExpressionConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ExpressionConverter/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.ExpressionConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ExpressionConverter/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.ExpressionConverter
```

## Usage

```csharp
using JorgeCostaMacia.ExpressionConverter.Domain;

// Expression -> dictionary
Dictionary<string, string> filter =
    ExpressionConverter.Convert<Order>(x => x.Status == "Open" && x.Amount == 100);
// { ["Status"] = "Open", ["Amount"] = "100" }

// dictionary -> expression
Expression<Func<Order, bool>> predicate =
    ExpressionConverter.ConvertBack<Order>(filter);
```

`Convert` expects equality checks combined with `&&`, with the property on the left and a constant on the right. `ConvertBack` rebuilds the predicate, converting each string back to the property's type — including `Guid`, enums and `Nullable<T>` (an empty string maps to `null`).

## Requirements

One of the following SDKs: **.NET 6 / 7 / 8 / 9 / 10** *(.NET 10 recommended)*.

## About

`JorgeCostaMacia.ExpressionConverter` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
