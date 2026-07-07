using System.Reflection;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;
using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore;

namespace JorgeCostaMacia.ValueObject.EfConverter;

/// <summary>
/// Convention helper that auto-applies the right per-family value converter to every single-value value
/// object, so a <c>DbContext</c> maps them all without a per-property call.
/// </summary>
public static class ValueObjectConversionExtensions
{
    /// <summary>The value-object family bases, each mapped to its open-generic converter.</summary>
    private static readonly (Type Base, Type Converter)[] Families =
    [
        (typeof(StringValueObject), typeof(StringValueObjectConverter<>)),
        (typeof(IntValueObject), typeof(IntValueObjectConverter<>)),
        (typeof(LongValueObject), typeof(LongValueObjectConverter<>)),
        (typeof(DecimalValueObject), typeof(DecimalValueObjectConverter<>)),
        (typeof(DoubleValueObject), typeof(DoubleValueObjectConverter<>)),
        (typeof(FloatValueObject), typeof(FloatValueObjectConverter<>)),
        (typeof(BoolValueObject), typeof(BoolValueObjectConverter<>)),
        (typeof(ByteValueObject), typeof(ByteValueObjectConverter<>)),
        (typeof(DateTimeValueObject), typeof(DateTimeValueObjectConverter<>)),
        (typeof(UuidValueObject), typeof(UuidValueObjectConverter<>))
    ];

    /// <summary>
    /// Registers the matching family converter for every single-value value object found in the given
    /// <paramref name="assemblies"/> — every concrete type deriving from one of the primitive value-object
    /// bases (multi-field value objects such as ranges derive from none, so they are skipped). Call it from
    /// <see cref="DbContext.ConfigureConventions(ModelConfigurationBuilder)"/>. The same converter serves
    /// required and nullable properties.
    /// </summary>
    /// <remarks>
    /// Applies to every property of those types across the model — value columns and foreign keys included.
    /// A value object used as a <b>primary key</b> is best configured explicitly (with
    /// <c>ValueGeneratedNever()</c>, since a converted key is app-assigned), as the convention does not set
    /// key value-generation.
    /// </remarks>
    /// <param name="builder">The model configuration builder (from <c>ConfigureConventions</c>).</param>
    /// <param name="assemblies">The assemblies to scan for value objects.</param>
    /// <returns>The same builder, to allow chaining.</returns>
    public static ModelConfigurationBuilder AddValueObjectConversions(this ModelConfigurationBuilder builder, params Assembly[] assemblies)
    {
        foreach (Type valueObject in assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => type is { IsClass: true, IsAbstract: false }))
        {
            foreach ((Type @base, Type converter) in Families)
            {
                if (!@base.IsAssignableFrom(valueObject)) continue;

                builder.Properties(valueObject).HaveConversion(converter.MakeGenericType(valueObject));

                break;
            }
        }

        return builder;
    }
}
