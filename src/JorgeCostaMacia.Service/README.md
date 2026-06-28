# JorgeCostaMacia.Service

Marker interfaces to tag and auto-register **services** in dependency injection. **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Service.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Service/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Service.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Service/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Service
```

## Usage

```csharp
using JorgeCostaMacia.Service.Domain;

// Tag a service so it can be discovered and registered (e.g. via assembly scanning).
public interface IOrderService : IService { }

// Tag strongly-typed options used to configure a service.
public sealed class OrderServiceOptions : IOptionsService
{
    public string ConnectionString { get; init; } = "";
}
```

- **`IService`** — marks a class/interface as a service (domain, application or infrastructure). Stateless/transient, DI-registered. Entities, value objects and DTOs should **not** implement it.
- **`IOptionsService`** — marks a strongly-typed options class used to declare/configure a service.

## Requirements

One of the following SDKs: **.NET 6 / 7 / 8 / 9 / 10** *(.NET 10 recommended)*.

## About

Part of the **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** toolkit — a modular set of DDD / CQRS / messaging building blocks.

- [Repository & full docs](https://github.com/JorgeCostaMacia/shared-net)
- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/) · [GitHub](https://github.com/JorgeCostaMacia/) · [Bitbucket](https://bitbucket.org/jorgecostamacia/)
