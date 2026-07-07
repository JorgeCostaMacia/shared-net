using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter for a <see cref="UuidValueObject"/>-based value object: stores it as its
/// underlying <see cref="Guid"/> and rebuilds it through its constructor on read. Provider-agnostic.
/// Works for both required and nullable properties (EF maps a null reference to a NULL column and
/// only invokes the converter for non-null values).
/// </summary>
/// <typeparam name="TValueObject">The value object type (deriving from <see cref="UuidValueObject"/>).</typeparam>
public sealed class UuidValueObjectConverter<TValueObject> : ValueConverter<TValueObject, Guid>
    where TValueObject : UuidValueObject
{
    /// <summary>Creates the converter.</summary>
    public UuidValueObjectConverter() : base(v => v.Value, ValueObjectConstructor.From<TValueObject, Guid>()) { }
}
