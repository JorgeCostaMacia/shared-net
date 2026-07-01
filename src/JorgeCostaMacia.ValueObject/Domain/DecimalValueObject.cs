namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="decimal"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on precise numeric values (e.g., Money, Quantity, Price).
/// It guarantees immutability and provides robust static factory methods for conversion from various primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record DecimalValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="decimal"/> value encapsulated by this Value Object.
    /// </summary>
    public decimal Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The decimal value to encapsulate.</param>
    public DecimalValueObject(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance from an existing decimal value (identity conversion).
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(decimal value) => new DecimalValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by parsing a string representation of the number.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a decimal.</exception>
    public static DecimalValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by converting an integer value.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(int value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by converting a float value.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by converting a boolean value (true maps to 1, false maps to 0).
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(bool value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by converting a long value.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(long value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DecimalValueObject"/> instance by converting a double value (may lose precision).
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject Create(double value) => Create(Convert(value));

    /// <summary>
    /// Converts a decimal value (identity conversion).
    /// </summary>
    protected static decimal Convert(decimal value) => value;

    /// <summary>
    /// Parses a string into a decimal value, trimming whitespace first.
    /// </summary>
    protected static decimal Convert(string value) => Convert(decimal.Parse(value.Trim()));

    /// <summary>
    /// Converts an integer to a decimal value.
    /// </summary>
    protected static decimal Convert(int value) => Convert((decimal)value);

    /// <summary>
    /// Converts a float to a decimal value.
    /// </summary>
    protected static decimal Convert(float value) => Convert(System.Convert.ToDecimal(value));

    /// <summary>
    /// Converts a boolean to a decimal value (true = 1, false = 0).
    /// </summary>
    protected static decimal Convert(bool value) => Convert(value ? 1 : 0);

    /// <summary>
    /// Converts a long to a decimal value.
    /// </summary>
    protected static decimal Convert(long value) => Convert((decimal)value);

    /// <summary>
    /// Converts a double to a decimal value (may involve overflow).
    /// </summary>
    protected static decimal Convert(double value) => Convert(System.Convert.ToDecimal(value));

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated decimal value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
