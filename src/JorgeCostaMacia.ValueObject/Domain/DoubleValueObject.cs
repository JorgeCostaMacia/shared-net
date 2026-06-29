namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="double"/> (double-precision floating-point) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on double-precision numeric values.
/// It guarantees immutability and provides static factory methods for conversion from various primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record DoubleValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="double"/> value encapsulated by this Value Object.
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The double value to encapsulate.</param>
    public DoubleValueObject(double value)
    {
        Value = value;
    }

    /// <summary>Creates a new <see cref="DoubleValueObject"/> from an existing double value (identity conversion).</summary>
    public static DoubleValueObject Create(double value) => new DoubleValueObject(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by parsing a string representation of the number.</summary>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a double.</exception>
    public static DoubleValueObject Create(string value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by converting an integer value.</summary>
    public static DoubleValueObject Create(int value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by converting a long value.</summary>
    public static DoubleValueObject Create(long value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by converting a float value.</summary>
    public static DoubleValueObject Create(float value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by converting a decimal value (may lose precision).</summary>
    public static DoubleValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>Creates a new <see cref="DoubleValueObject"/> by converting a boolean value (true maps to 1, false maps to 0).</summary>
    public static DoubleValueObject Create(bool value) => Create(Convert(value));

    /// <summary>Converts a double value (identity conversion).</summary>
    protected static double Convert(double value) => value;

    /// <summary>Parses a string into a double value, trimming whitespace first.</summary>
    protected static double Convert(string value) => Convert(double.Parse(value.Trim()));

    /// <summary>Converts an integer to a double value.</summary>
    protected static double Convert(int value) => Convert((double)value);

    /// <summary>Converts a long to a double value.</summary>
    protected static double Convert(long value) => Convert((double)value);

    /// <summary>Converts a float to a double value.</summary>
    protected static double Convert(float value) => Convert((double)value);

    /// <summary>Converts a decimal to a double value (may lose precision).</summary>
    protected static double Convert(decimal value) => Convert((double)value);

    /// <summary>Converts a boolean to a double value (true = 1, false = 0).</summary>
    protected static double Convert(bool value) => Convert(value ? 1d : 0d);

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated double value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
