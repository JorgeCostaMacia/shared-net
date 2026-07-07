using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter for an <see cref="IntValueObject"/>-based value object: stores it as its
/// underlying <see cref="int"/> and rebuilds it through its constructor on read. Provider-agnostic.
/// Works for both required and nullable properties (EF maps a null reference to a NULL column and
/// only invokes the converter for non-null values).
/// </summary>
/// <typeparam name="TValueObject">The value object type (deriving from <see cref="IntValueObject"/>).</typeparam>
public sealed class IntValueObjectConverter<TValueObject> : ValueConverter<TValueObject, int>
    where TValueObject : IntValueObject
{
    /// <summary>Creates the converter.</summary>
    public IntValueObjectConverter() : base(v => v.Value, ValueObjectConstructor.From<TValueObject, int>()) { }
}
