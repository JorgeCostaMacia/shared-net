# JorgeCostaMacia.ValueObject

Immutable, type-safe **value objects** for the common primitives — `int`, `string`, `bool`, `byte`, `decimal`, `float`, `DateTime`, `Guid` — each with `Create(...)` conversion factories, value equality, and an implicit operator back to the primitive. Ships with a FluentValidation validator and a typed validation exception per type.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.ValueObject.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.ValueObject.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.ValueObject
```

## Usage

```csharp
using JorgeCostaMacia.ValueObject.Domain;

IntValueObject age = IntValueObject.Create("42");   // converts from string, int, long, float, double, decimal, bool
int raw = age;                                        // implicit operator back to int

UuidValueObject id = UuidValueObject.Create(Guid.NewGuid());
```

### Validation lives at the call site

Value objects don't validate themselves — you validate **where you use them**, so it's explicit. Each type ships a FluentValidation validator and a typed `…ValidationException`; add your rules in your own validator (or the provided one) and run it:

```csharp
validator.ValidateAndThrow(age);   // throws IntValueObjectValidationException on failure
```

## Requirements

One of the following SDKs: **.NET 6 / 7 / 8 / 9 / 10** *(.NET 10 recommended)*.

Depends on [JorgeCostaMacia.Exception](https://www.nuget.org/packages/JorgeCostaMacia.Exception/) and [FluentValidation](https://www.nuget.org/packages/FluentValidation/).

## About

`JorgeCostaMacia.ValueObject` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
