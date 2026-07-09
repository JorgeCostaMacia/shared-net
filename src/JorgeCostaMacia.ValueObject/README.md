# JorgeCostaMacia.ValueObject

Immutable, type-safe **value objects** — primitives (`int`, `long`, `float`, `double`, `decimal`, `bool`, `byte`, `string`, `Guid`, `DateTime`, UTC `DateTime`), semantic strings (email, URL, IP, JSON), numeric/date ranges, and paging/ordering — each with the three-verb creation surface (constructor hydrates, `From(...)` converts unvalidated, `Create(...)` fabricates validated) and value equality. Every type ships a FluentValidation validator and a typed validation exception.

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

EmailValueObject email = EmailValueObject.Create("user@host.com");   // validated: nothing invalid escapes Create
EmailValueObject draft = EmailValueObject.From("  user@host.com ");  // normalized but UNVALIDATED (composites' path)
string raw = email.Value;                                            // access the underlying value explicitly
string same = email;                                                 // or implicitly (VO -> primitive operator)

UuidValueObject id = UuidValueObject.Create(Guid.NewGuid());
DateTimeUtcValueObject when = DateTimeUtcValueObject.Create(DateTime.UtcNow);   // tags Utc kind; convert wall-clock at the call site
```

### The three-verb creation surface

Each value object exposes exactly three ways in, on its **natural primitive** (convert other types at the call site):

- **`new X(value)`** — hydrates raw, no normalization, no validation. Reserved for infrastructure (ORMs, deserializers — the EF converters use it).
- **`X.From(value)`** — converts: normalizes and materializes, **unvalidated**. Composites build their parts through it so the whole object validates in one pass.
- **`X.Create(value)`** — fabricates validated: runs the type's validator and throws its typed `…ValidationException` (with the **complete** failure list) on violation.

Each type's validator also assembles itself: `EmailValueObjectValidator.Create()` chains the `Create()` of the validators it composes — or take the constructor and inject the composition via DI:

```csharp
EmailValueObjectValidator.Create().ValidateAndThrow(email);   // throws EmailValueObjectValidationException on failure
```

### Register the validators

```csharp
services.AddValueObjectContext();   // registers every IValidator<…> for the value objects
```

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

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
