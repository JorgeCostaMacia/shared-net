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
/// family carries the conversion logic from other primitive types (the known tokens for true and for false;
/// anything else throws), so Value Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record BoolValueObject : IValueObject
{
    private static readonly List<string> _true = new List<string>() { "TRUE", "1", "SI", "SÍ", "S", "YES", "Y" };
    private static readonly List<string> _false = new List<string>() { "FALSE", "0", "NO", "N" };

    /// <summary>
    /// The immutable <see cref="bool"/> value encapsulated by this Value Object.
    /// </summary>
    public bool Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The boolean value to encapsulate.</param>
    public BoolValueObject(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="BoolValueObject"/> from the natural primitive through
    /// <see cref="Convert(bool)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new, unvalidated <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject From(bool value) => new BoolValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is
    /// <see langword="null"/>, otherwise
    /// the same result as <see cref="From(bool)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The boolean value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="BoolValueObject"/> instance, or <see langword="null"/>.</returns>
    public static BoolValueObject? FromOrNull(bool? value) => value is null ? null : From(value.Value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(bool)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new, validated <see cref="BoolValueObject"/> instance.</returns>
    /// <exception cref="BoolValueObjectValidationException">Thrown when the resulting value violates a validation
    /// rule.</exception>
    public static BoolValueObject Create(bool value)
    {
        BoolValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is
    /// <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(bool)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The boolean value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="BoolValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="BoolValueObjectValidationException">Thrown when a supplied value violates a validation
    /// rule.</exception>
    public static BoolValueObject? CreateOrNull(bool? value)
    {
        BoolValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => BoolValueObjectValidator.Create().ValidateAndThrow(this);

    /// <summary>
    /// Converts a boolean value (identity conversion).
    /// </summary>
    protected static bool Convert(bool value) => value;

    /// <summary>
    /// Converts a string to a boolean, trimmed and case-insensitive: <c>TRUE, 1, SI, SÍ, S, YES, Y</c> are true and
    /// <c>FALSE, 0, NO, N</c> are false. Anything else throws <see cref="FormatException"/> rather than being read as
    /// false, which would turn every <c>S</c> of an S/N field into a false.
    /// </summary>
    protected static bool Convert(string value)
    {
        string token = value.Trim().ToUpperInvariant();

        if (_true.Contains(token))
        {
            return true;
        }

        if (_false.Contains(token))
        {
            return false;
        }

        throw new FormatException($"{value} is not a boolean");
    }

    /// <summary>
    /// Converts an integer to a boolean: <c>1</c> is true, <c>0</c> is false, anything else throws
    /// <see cref="FormatException"/>.
    /// </summary>
    protected static bool Convert(int value) => Convert((decimal)value);

    /// <summary>
    /// Converts a float to a boolean: <c>1</c> is true, <c>0</c> is false, anything else throws
    /// <see cref="FormatException"/>.
    /// Widening to double is exact, so nothing is rounded on the way.
    /// </summary>
    protected static bool Convert(float value) => Convert((double)value);

    /// <summary>
    /// Converts a long to a boolean: <c>1</c> is true, <c>0</c> is false, anything else throws
    /// <see cref="FormatException"/>.
    /// </summary>
    protected static bool Convert(long value) => Convert((decimal)value);

    /// <summary>
    /// Converts a double to a boolean: <c>1</c> is true, <c>0</c> is false, anything else throws
    /// <see cref="FormatException"/>.
    /// Compared on the double itself, not through decimal, which keeps only 15 significant digits.
    /// </summary>
    protected static bool Convert(double value)
    {
        if (value == 1)
        {
            return true;
        }

        if (value == 0)
        {
            return false;
        }

        throw new FormatException($"{value} is not a boolean");
    }

    /// <summary>
    /// Converts a decimal to a boolean: <c>1</c> is true, <c>0</c> is false, anything else throws
    /// <see cref="FormatException"/>.
    /// </summary>
    protected static bool Convert(decimal value)
    {
        if (value == 1)
        {
            return true;
        }

        if (value == 0)
        {
            return false;
        }

        throw new FormatException($"{value} is not a boolean");
    }

    /// <summary>Implicitly converts the value object to its underlying <see cref="bool"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator bool(BoolValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated boolean value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string ("True" or "False").</returns>
    public sealed override string ToString() => Value.ToString();
}
