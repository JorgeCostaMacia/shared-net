using System.Globalization;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="long"/> (64-bit signed integer) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on 64-bit integer logic (e.g., large IDs, counters).
/// It guarantees immutability and provides static factory methods for conversion from various primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record LongValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="long"/> value encapsulated by this Value Object.
    /// </summary>
    public long Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The long value to encapsulate.</param>
    public LongValueObject(long value) => Value = value;

    /// <summary>Creates a new <see cref="LongValueObject"/> from an existing long value (identity conversion).</summary>
    public static LongValueObject Create(long value) => new LongValueObject(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by parsing a string representation of the number.</summary>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a number.</exception>
    /// <exception cref="OverflowException">Thrown if the parsed value is out of <see cref="long"/> range.</exception>
    public static LongValueObject Create(string value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by converting an integer value.</summary>
    public static LongValueObject Create(int value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by converting a float value (involving truncation).</summary>
    public static LongValueObject Create(float value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by converting a double value (involving truncation).</summary>
    public static LongValueObject Create(double value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by converting a decimal value (involving truncation).</summary>
    public static LongValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="LongValueObject"/> by converting a boolean value (true maps to 1, false maps to 0).</summary>
    public static LongValueObject Create(bool value) => Create(Convert(value));

    /// <summary>Converts a long value (identity conversion).</summary>
    protected static long Convert(long value) => value;

    /// <summary>Parses a string into a decimal, then truncates it to a long — the fractional part is dropped toward zero, never rounded (trimming whitespace first).</summary>
    protected static long Convert(string value) => Convert(decimal.Parse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture));

    /// <summary>Converts an integer to a long value (widening; exact).</summary>
    protected static long Convert(int value) => value;

    /// <summary>Converts a float to a long value, truncating the fractional part toward zero.</summary>
    protected static long Convert(float value) => Convert((decimal)value);

    /// <summary>Converts a double to a long value, truncating the fractional part toward zero.</summary>
    protected static long Convert(double value) => Convert((decimal)value);

    /// <summary>Converts a decimal to a long value, truncating the fractional part toward zero. Throws <see cref="OverflowException"/> if out of <see cref="long"/> range.</summary>
    protected static long Convert(decimal value) => (long)value;

    /// <summary>Converts a boolean to a long value (true = 1, false = 0).</summary>
    protected static long Convert(bool value) => value ? 1L : 0L;

    /// <summary>Implicitly converts the value object to its underlying <see cref="long"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator long(LongValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated long value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
