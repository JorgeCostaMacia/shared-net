using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter for a <see cref="DateTimeValueObject"/>-based value object: stores it as its
/// underlying <see cref="DateTime"/> and rebuilds it through its constructor on read. Provider-agnostic.
/// Works for both required and nullable properties (EF maps a null reference to a NULL column and
/// only invokes the converter for non-null values).
/// </summary>
/// <typeparam name="TValueObject">The value object type (deriving from <see cref="DateTimeValueObject"/>).</typeparam>
public sealed class DateTimeValueObjectConverter<TValueObject> : ValueConverter<TValueObject, DateTime>
    where TValueObject : DateTimeValueObject
{
    /// <summary>Creates the converter.</summary>
    public DateTimeValueObjectConverter() : base(valueObject => valueObject.Value, ValueObjectConstructor.From<TValueObject, DateTime>()) { }
}
