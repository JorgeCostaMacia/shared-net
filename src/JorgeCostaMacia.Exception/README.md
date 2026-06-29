# JorgeCostaMacia.Exception

A small hierarchy of **domain exceptions** carrying traceable metadata — a stable id, type name, error code, HTTP status code and UTC timestamp — for consistent logging, correlation and API error mapping.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Exception.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Exception/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Exception.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Exception/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Exception
```

## Types

All are `abstract` — inherit them in your bounded context to create concrete, named exceptions.

| Type | For | Default HTTP |
| --- | --- | --- |
| `DomainException` | base of the hierarchy | 500 |
| `ErrorException` | one or more general business errors (`Errors`) | 400 |
| `ExistException` | resource already exists / conflict | 409 |
| `NotFoundException` | resource not found | 404 |
| `ValidationException` | validation failures (`Validations`) | 400 |

## Usage

```csharp
using JorgeCostaMacia.Exception.Domain;

public sealed class CustomerNotFoundException(Guid customerId)
    : NotFoundException(message: $"Customer {customerId} was not found.");

// metadata is auto-filled: AggregateId (UUIDv7 via GuidFactory), AggregateType,
// AggregateCode, AggregateHttpCode (404), AggregateOccurredAt (UTC).
```

`ValidationException` carries a list of `FluentValidation.Results.ValidationFailure`, so it plugs straight into a FluentValidation `AbstractValidator`:

```csharp
protected override void RaiseValidationException(ValidationContext<T> context, ValidationResult result)
    => throw new CustomerValidationException(result.Errors);
```

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

Depends on [JorgeCostaMacia.GuidFactory](https://www.nuget.org/packages/JorgeCostaMacia.GuidFactory/) and [FluentValidation](https://www.nuget.org/packages/FluentValidation/).

## About

`JorgeCostaMacia.Exception` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
