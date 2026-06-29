# JorgeCostaMacia.DomainEvent

Marker interface (`IDomainEvent`) for a **transport-agnostic domain event** — an immutable fact about something that happened in the domain, raised by aggregates. **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.DomainEvent.svg)](https://www.nuget.org/packages/JorgeCostaMacia.DomainEvent/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.DomainEvent.svg)](https://www.nuget.org/packages/JorgeCostaMacia.DomainEvent/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.DomainEvent
```

## Usage

```csharp
using JorgeCostaMacia.DomainEvent.Domain;

public record OrderCreated(Guid OrderId) : IDomainEvent;
```

The domain depends only on this marker — it doesn't know how (or whether) the event travels over a bus. A messaging layer can specialize it (`IEvent : IDomainEvent, IMessage, ...`) so concrete events fit an aggregate's event list while the domain stays transport-free.

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

## About

`JorgeCostaMacia.DomainEvent` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
