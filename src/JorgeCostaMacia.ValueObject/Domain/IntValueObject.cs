namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="int"/> (32-bit signed integer) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on integer logic (e.g., ID, Count, Age).
/// It guarantees immutability and provides robust static factory methods for conversion from various primitive types,
/// often involving truncation or explicit casting during conversion.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record IntValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="int"/> value encapsulated by this Value Object.
    /// </summary>
    public int Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The integer value to encapsulate.</param>
    public IntValueObject(int value) => Value = value;

    /// <summary>Implicitly converts the value object to its underlying <see cref="int"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator int(IntValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance from an existing integer value (identity conversion).
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(int value) => new IntValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a string representation of the number.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a number.</exception>
    public static IntValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a float value (involving truncation).
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a decimal value (involving truncation).
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a boolean value (true maps to 1, false maps to 0).
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(bool value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a long value (may involve truncation or overflow).
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(long value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IntValueObject"/> instance by converting a double value (involving truncation).
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject Create(double value) => Create(Convert(value));

    /// <summary>
    /// Converts an integer value (identity conversion).
    /// </summary>
    protected static int Convert(int value) => value;

    /// <summary>
    /// Parses a string into a float, then converts it to an integer (trimming whitespace first). This conversion may lose fractional data.
    /// </summary>
    protected static int Convert(string value) => Convert(System.Convert.ToInt32(float.Parse(value.Trim())));

    /// <summary>
    /// Converts a float to an integer (may involve truncation).
    /// </summary>
    protected static int Convert(float value) => Convert(System.Convert.ToInt32(value));

    /// <summary>
    /// Converts a decimal to an integer (may involve truncation).
    /// </summary>
    protected static int Convert(decimal value) => Convert(System.Convert.ToInt32(value));

    /// <summary>
    /// Converts a boolean to an integer (1 for true, 0 for false).
    /// </summary>
    protected static int Convert(bool value) => Convert(System.Convert.ToInt32(value));

    /// <summary>
    /// Converts a long to an integer (may involve overflow).
    /// </summary>
    protected static int Convert(long value) => Convert(System.Convert.ToInt32(value));

    /// <summary>
    /// Converts a double to an integer (may involve truncation).
    /// </summary>
    protected static int Convert(double value) => Convert(System.Convert.ToInt32(value));

    /// <summary>
    /// Returns the string representation of the encapsulated integer value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
