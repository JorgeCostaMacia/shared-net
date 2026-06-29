# JorgeCostaMacia.GuidMySqlConverter

Converts a `Guid` to/from the **big-endian (RFC 4122) byte layout** used by **MySQL / MariaDB** `BINARY(16)` columns, instead of .NET's SQL Server-native little-endian layout. **Zero dependencies.**

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.GuidMySqlConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.GuidMySqlConverter/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.GuidMySqlConverter.svg)](https://www.nuget.org/packages/JorgeCostaMacia.GuidMySqlConverter/)
[![Build](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/shared-net/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/shared-net.svg)](https://github.com/JorgeCostaMacia/shared-net/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.GuidMySqlConverter
```

## Usage

```csharp
using JorgeCostaMacia.GuidMySqlConverter.Infrastructure;

byte[] forDb = GuidMySqlConverter.ConvertToBytes(id);   // store in a BINARY(16) column
Guid back    = GuidMySqlConverter.ConvertFromBytes(forDb);
```

.NET's `Guid.ToByteArray()` matches SQL Server's `uniqueidentifier` layout, and PostgreSQL handles GUIDs natively — neither needs this. **MySQL / MariaDB** store GUIDs as `BINARY(16)` and expect the first three fields in big-endian (string) order.

### With EF Core

This package is dependency-free on purpose. Wrap it in a `ValueConverter` **in your own solution**, bound to your EF Core version — so this package never forces an EF Core version on you:

```csharp
public sealed class MySqlGuidConverter() : ValueConverter<Guid, byte[]>(
    guid => GuidMySqlConverter.ConvertToBytes(guid),
    bytes => GuidMySqlConverter.ConvertFromBytes(bytes));

// entity.Property(e => e.Id).HasColumnType("binary(16)").HasConversion<MySqlGuidConverter>();
```

## Requirements

One of the following SDKs: **.NET 8 / 9 / 10** *(.NET 10 recommended)*.

## About

`JorgeCostaMacia.GuidMySqlConverter` is part of **[shared-net](https://github.com/JorgeCostaMacia/shared-net)** — a set of foundational, self-contained .NET packages, each scoped to a single concern and reusable across your bounded contexts.

- **Repository:** [github.com/JorgeCostaMacia/shared-net](https://github.com/JorgeCostaMacia/shared-net)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/shared-net/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/shared-net/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/shared-net/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
