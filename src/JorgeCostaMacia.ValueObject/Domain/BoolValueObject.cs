using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="bool"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on boolean logic (e.g., IsActive, IsPublished).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(bool)"/> converts (materializes, unvalidated) and
/// <see cref="Create(bool)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="bool"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types (e.g. the truthy tokens "TRUE", "1",
/// "SI", "YES"), so Value Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record BoolValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="bool"/> value encapsulated by this Value Object.
    /// </summary>
    public bool Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The boolean value to encapsulate.</param>
    public BoolValueObject(bool value) => Value = value;

    /// <summary>
    /// Converts: materializes a new <see cref="BoolValueObject"/> from the natural primitive through
    /// <see cref="Convert(bool)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new, unvalidated <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject From(bool value) => new BoolValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(bool)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new, validated <see cref="BoolValueObject"/> instance.</returns>
    /// <exception cref="BoolValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static BoolValueObject Create(bool value)
    {
        BoolValueObject vo = From(value);
        BoolValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts a boolean value (identity conversion).
    /// </summary>
    protected static bool Convert(bool value) => value;

    /// <summary>
    /// Converts a string to a boolean. Supports "TRUE", "1", "SI", or "YES" (case-insensitive).
    /// </summary>
    protected static bool Convert(string value) => Convert(value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "1" || value.Trim().ToUpper() == "SI" || value.Trim().ToUpper() == "YES");

    /// <summary>
    /// Converts an integer to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(int value) => Convert(value == 1);

    /// <summary>
    /// Converts a float to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(float value) => Convert((int)value == 1);

    /// <summary>
    /// Converts a long to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(long value) => Convert(value == 1);

    /// <summary>
    /// Converts a double to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(double value) => Convert((int)value == 1);

    /// <summary>
    /// Converts a decimal to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(decimal value) => Convert((int)value == 1);

    /// <summary>Implicitly converts the value object to its underlying <see cref="bool"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator bool(BoolValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated boolean value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string ("True" or "False").</returns>
    public override string ToString() => Value.ToString();
}
