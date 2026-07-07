using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter for a <see cref="StringValueObject"/>-based value object: stores it as its
/// underlying <see cref="string"/> and rebuilds it through its constructor on read. Provider-agnostic.
/// Works for both required and nullable properties (EF maps a null reference to a NULL column and
/// only invokes the converter for non-null values).
/// </summary>
/// <typeparam name="TValueObject">The value object type (deriving from <see cref="StringValueObject"/>).</typeparam>
public sealed class StringValueObjectConverter<TValueObject> : ValueConverter<TValueObject, string>
    where TValueObject : StringValueObject
{
    /// <summary>Creates the converter.</summary>
    public StringValueObjectConverter() : base(v => v.Value, ValueObjectConstructor.From<TValueObject, string>()) { }
}
