# JorgeCostaMacia.ValueObject

Immutable, type-safe **value objects** — primitives (`int`, `long`, `float`, `double`, `decimal`, `bool`, `byte`, `string`, `Guid`, `DateTime`, UTC `DateTime`), semantic strings (email, URL, IP, JSON), and paging/ordering — each with the same creation surface (constructor hydrates, `From(...)` converts unvalidated, `Create(...)` fabricates validated, and an `OrNull` sibling for each factory) and value equality. Every type ships a FluentValidation validator and a typed validation exception.

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
EmailValueObject draft = EmailValueObject.From("  user@host.com ");  // normalized but UNVALIDATED
EmailValueObject? none = EmailValueObject.CreateOrNull(null);         // absence propagates: null in, null out
string raw = email.Value;                                            // access the underlying value explicitly
string same = email;                                                 // or implicitly (VO -> primitive operator)

UuidValueObject id = UuidValueObject.Create(Guid.NewGuid());
DateTimeUtcValueObject when = DateTimeUtcValueObject.Create(DateTime.UtcNow);   // tags Utc kind; convert wall-clock at the call site
```

### The creation surface

Each value object exposes three verbs on its **natural primitive** (convert other types at the call site), and the two that take a value or nothing:

- **`new X(value)`** — hydrates raw, no normalization, no validation. Reserved for infrastructure (ORMs, deserializers — the EF converters use it).
- **`X.From(value)`** — converts: normalizes and materializes, **unvalidated**. A composite in your own domain builds its parts through it, so the whole object validates in one pass instead of throwing part by part.
- **`X.Create(value)`** — fabricates validated: runs the type's validator and throws its typed `…ValidationException` (with the **complete** failure list) on violation.
- **`X.FromOrNull(value)`** / **`X.CreateOrNull(value)`** — the same two, for a field that may not come at all: `null` in, `null` out. They are about **absence, not failure** — a value that did come is still converted, and in `CreateOrNull` still validated. That is why there is no `TryFrom` (`From` cannot fail, so there is nothing to try) and no `FromOrDefault` (coercing an out-of-range value would hide the caller's mistake).

Each type's validator also assembles itself: `EmailValueObjectValidator.Create()` chains the `Create()` of the validators it composes — or take the constructor and inject the composition via DI:

```csharp
EmailValueObjectValidator.Create().ValidateAndThrow(email);   // throws EmailValueObjectValidationException on failure
```

### Paging arithmetic

`PageSizeValueObject` and `PageNumberValueObject` carry the two calculations every paged query
rewrites by hand, and the two places that arithmetic goes wrong — the division that loses the last,
partial page, and the offset that misses by one whole page:

```csharp
PageSizeValueObject size = PageSizeValueObject.Create(10);
PageNumberValueObject page = PageNumberValueObject.Create(99);

int pages  = size.Pages(25);      // 3 — the ceiling division, so the partial last page survives
int offset = size.Offset(2);      // 10 — rows to skip to reach page 2; the first page skips none
int bound  = page.Within(pages);  // 3 — a page past the last resolves to the last

query.Skip(size.Offset(bound)).Take(size.Value);
```

They are total: every input gets an answer and none of them throws. Construction is where a value
object decides what is valid — these compute on a value that already is.

### Deriving your own value object

Derive from the matching base and keep the type inside the contract — a **public hydration constructor** plus your own **`From`**, **`FromOrNull`**, **`Create`** and **`CreateOrNull`**, each on the natural primitive. All four need `static new`: the inherited ones return the *base* type, and the compiler proves each keyword is earned — `CS0108` fails a declaration that omits it, `CS0109` one that adds it where nothing is hidden.

```csharp
public sealed record ClientName : StringValueObject
{
    public ClientName(string value) : base(value) { }   // hydration ctor

    // Convert is the base's protected cleanser
    public static new ClientName From(string value) => new ClientName(Convert(value));

    public static new ClientName? FromOrNull(string? value) => value is null ? null : From(value);

    public static new ClientName Create(string value)
    {
        ClientName vo = From(value);
        vo.Validate();

        return vo;
    }

    public static new ClientName? CreateOrNull(string? value)
    {
        ClientName? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    // one line per value object, mirroring the bases: Create reads as From + Validate
    private void Validate() => ClientNameValidator.Create().ValidateAndThrow(this);
}
```

This surface is **required**, not optional: the ecosystem depends on it — the EF converters rehydrate through the constructor, deserializers too — so every value object here carries it and a contract test keeps it that way.

### No DI registration needed

Each validator assembles itself through its static `Create()`, chaining the `Create()` of the validators
it includes, so a value object's `Create()` reaches its rules with nothing registered anywhere:

```csharp
EmailValueObject email = EmailValueObject.Create("user@host.com");   // its validator builds itself
```

The constructors stay public, so a consumer that wants a validator from its own container can register
one — `services.AddScoped<IValidator<EmailValueObject>, EmailValueObjectValidator>()` — and inject the
`IValidator<StringValueObject>` it composes. This package registers nothing on your behalf, and takes
no dependency on the DI abstractions.

## Requirements

The **.NET 10** SDK.

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
