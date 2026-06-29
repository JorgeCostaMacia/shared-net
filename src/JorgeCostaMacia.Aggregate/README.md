# JorgeCostaMacia.Aggregate

The DDD **Aggregate Root** building block: a base class (and `IAggregate` contract) that accumulates **domain events** raised during a unit of work, then hands them off — pull-and-clear — for persistence and publication.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Aggregate.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Aggregate/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Aggregate.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Aggregate/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Aggregate
```

## Usage

```csharp
using JorgeCostaMacia.Aggregate.Domain;
using JorgeCostaMacia.DomainEvent.Domain;

public sealed class Order : Aggregate
{
    public void Place()
    {
        // ... mutate state ...
        AddAggregateEvents(new OrderPlaced(Id));
    }
}

// after handling a command, the persistence/publish layer drains the events exactly once:
IEnumerable<IDomainEvent> events = order.PullAggregateEvents();
```

## Transport-agnostic by design

The aggregate accumulates **`IDomainEvent`** — a pure marker from [JorgeCostaMacia.DomainEvent](https://www.nuget.org/packages/JorgeCostaMacia.DomainEvent/) with no knowledge of any bus. A messaging layer specializes it (`IEvent : IDomainEvent, ...`), so concrete events still fit the aggregate's event list while the domain stays free of any transport dependency.

`PullAggregateEvents()` returns the pending events **and clears** the internal list, so they are published exactly once per unit of work.

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

Depends on [JorgeCostaMacia.DomainEvent](https://www.nuget.org/packages/JorgeCostaMacia.DomainEvent/).

## About

`JorgeCostaMacia.Aggregate` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
