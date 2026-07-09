using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="Guid"/> (UUID) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on GUID/UUID logic (e.g., entity IDs, correlation IDs).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(Guid)"/> converts (materializes, unvalidated) and
/// <see cref="Create(Guid)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="Guid"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types, so Value Objects deriving from this one
/// in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record UuidValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="Guid"/> value encapsulated by this Value Object.
    /// </summary>
    public Guid Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The GUID value to encapsulate.</param>
    public UuidValueObject(Guid value) => Value = value;

    /// <summary>
    /// Converts: materializes a new <see cref="UuidValueObject"/> from the natural primitive through
    /// <see cref="Convert(Guid)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source GUID value.</param>
    /// <returns>A new, unvalidated <see cref="UuidValueObject"/> instance.</returns>
    public static UuidValueObject From(Guid value) => new UuidValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(Guid)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source GUID value.</param>
    /// <returns>A new, validated <see cref="UuidValueObject"/> instance.</returns>
    /// <exception cref="UuidValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static UuidValueObject Create(Guid value)
    {
        UuidValueObject vo = From(value);
        UuidValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts a GUID value (identity conversion).
    /// </summary>
    protected static Guid Convert(Guid value) => value;

    /// <summary>
    /// Converts a string to a GUID value.
    /// </summary>
    protected static Guid Convert(string value) => Convert(new Guid(value.Trim()));

    /// <summary>Implicitly converts the value object to its underlying <see cref="Guid"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator Guid(UuidValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated GUID value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a standard string representation of a GUID.</returns>
    public override string ToString() => Value.ToString();
}
