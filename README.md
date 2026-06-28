# shared-net

> A set of foundational, self-contained .NET packages — DDD building blocks and small utilities — each scoped to a single concern and shipped independently under `JorgeCostaMacia.*`.

[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](LICENSE.txt)
[![Main](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![Dev](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/dev.yml/badge.svg?branch=develop)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/dev.yml)

## Requirements

One of the following SDKs: **.NET 6 / 7 / 8 / 9 / 10** *(.NET 10 recommended)*.

## Packages

| Package | What it does |
| --- | --- |
| [JorgeCostaMacia.GuidFactory](https://www.nuget.org/packages/JorgeCostaMacia.GuidFactory/) | Time-ordered GUIDs (UUIDv7) with automatic v4 fallback. |
| [JorgeCostaMacia.GuidMySqlConverter](https://www.nuget.org/packages/JorgeCostaMacia.GuidMySqlConverter/) | Converts a `Guid` to/from the MySQL / MariaDB `BINARY(16)` byte layout. |
| [JorgeCostaMacia.ExpressionConverter](https://www.nuget.org/packages/JorgeCostaMacia.ExpressionConverter/) | Convert simple LINQ predicates to/from `Dictionary<string, string>`. |
| [JorgeCostaMacia.Entity](https://www.nuget.org/packages/JorgeCostaMacia.Entity/) | `IEntity` marker — identity-based DDD entities. |
| [JorgeCostaMacia.DomainEvent](https://www.nuget.org/packages/JorgeCostaMacia.DomainEvent/) | `IDomainEvent` marker — transport-agnostic domain events. |
| [JorgeCostaMacia.Aggregate](https://www.nuget.org/packages/JorgeCostaMacia.Aggregate/) | DDD Aggregate Root base that accumulates `IDomainEvent`s. |
| [JorgeCostaMacia.Exception](https://www.nuget.org/packages/JorgeCostaMacia.Exception/) | Domain exception hierarchy with traceable metadata. |

_More on the way — `ValueObject` and a `JorgeCostaMacia` umbrella meta-package (install-everything)._

## Contact

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
