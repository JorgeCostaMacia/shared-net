using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter for a <see cref="ByteValueObject"/>-based value object: stores it as its
/// underlying <see cref="byte"/>[] and rebuilds it through its constructor on read. Provider-agnostic.
/// Works for both required and nullable properties (EF maps a null reference to a NULL column and
/// only invokes the converter for non-null values).
/// </summary>
/// <typeparam name="TValueObject">The value object type (deriving from <see cref="ByteValueObject"/>).</typeparam>
public sealed class ByteValueObjectConverter<TValueObject> : ValueConverter<TValueObject, byte[]>
    where TValueObject : ByteValueObject
{
    /// <summary>Creates the converter.</summary>
    public ByteValueObjectConverter() : base(valueObject => valueObject.Value, ValueObjectConstructor.From<TValueObject, byte[]>()) { }
}
