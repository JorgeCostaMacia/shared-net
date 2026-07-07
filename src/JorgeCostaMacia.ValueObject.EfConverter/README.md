# JorgeCostaMacia.ValueObject.EfConverter

Native **EF Core value converters** for [`JorgeCostaMacia.ValueObject`](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject/) value objects — one converter per family (value object &lt;-&gt; underlying value) plus a convention that auto-applies them. **Provider-agnostic** (works with Npgsql, SQL Server, SQLite, …).

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.ValueObject.EfConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject.EfConverter/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.ValueObject.EfConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject.EfConverter/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.ValueObject.EfConverter
```

Brings [`JorgeCostaMacia.ValueObject`](https://www.nuget.org/packages/JorgeCostaMacia.ValueObject/) and `Microsoft.EntityFrameworkCore` (the provider-agnostic core — **not** a database provider). Your own provider (e.g. `Npgsql.EntityFrameworkCore.PostgreSQL`) builds on the same core, so there is no conflict; NuGet resolves EF Core to your provider's version.

## Usage

### The convention — map every value object at once

```csharp
protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    => builder.AddValueObjectConversions(typeof(SomeValueObject).Assembly);
```

Every single-value value object (any type deriving from `IntValueObject`, `StringValueObject`, `DecimalValueObject`, …) is stored as its underlying primitive automatically — value columns and foreign keys included. Multi-field value objects (ranges) are skipped.

### Or apply a converter explicitly

```csharp
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

builder.Property(e => e.Email).HasConversion(new StringValueObjectConverter<Email>());
```

The same converter serves **required and nullable** properties (`Email` and `Email?`): EF maps a null reference to a `NULL` column and only invokes the converter for non-null values.

### Value object as a primary key

A value object works as a key via its converter (complex/owned types cannot be keys). Configure it explicitly, since a converted key is app-assigned:

```csharp
builder.HasKey(e => e.Id);
builder.Property(e => e.Id)
    .HasConversion(new IntValueObjectConverter<UserId>())
    .ValueGeneratedNever();
```

## Notes

- **Rehydration goes through the value object's constructor** (found by reflection, compiled once) — **not** the `Create` factory — so reads do not re-run domain validation on already-persisted data. The constructor may be non-public.
- Querying by the underlying value uses the value object's conversion operator, e.g. `where (int)e.Id > 1000` or `where ((string)e.Name).StartsWith("a")`.
- **`byte[]`-backed value objects** (`ByteValueObject`): `byte[]` is a mutable reference type, so EF Core needs a [`ValueComparer`](https://learn.microsoft.com/ef/core/modeling/value-comparers) to detect changes by content rather than by reference. Value objects are immutable (you replace the whole object, which EF sees as a new reference), so this only matters if you mutate the array in place — an anti-pattern. If you do, configure a comparer on that property.

## About

Part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)**.

**Author:** Jorge Costa Maciá — [GitHub](https://github.com/JorgeCostaMacia/)
