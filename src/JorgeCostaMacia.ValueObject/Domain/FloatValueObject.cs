namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="float"/> (Single precision floating-point) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on numeric values that require floating-point arithmetic (e.g., coordinates, physical measurements).
/// It guarantees immutability and provides robust static factory methods for conversion from various primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record FloatValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="float"/> value encapsulated by this Value Object.
    /// </summary>
    public float Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The float value to encapsulate.</param>
    public FloatValueObject(float value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance from an existing float value (identity conversion).
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(float value) => new FloatValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by parsing a string representation of the number.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a float.</exception>
    public static FloatValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting an integer value.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(int value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting a decimal value.
    /// Note: This conversion can involve loss of precision.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting a boolean value (true maps to 1, false maps to 0).
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(bool value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting a long value.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(long value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting a double value (may lose precision).
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(double value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="FloatValueObject"/> instance by converting a <see cref="DateTime"/> value to the total seconds since its epoch (based on Ticks).
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value.</param>
    /// <returns>A new <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject Create(DateTime value) => Create(Convert(value));

    /// <summary>
    /// Converts a float value (identity conversion).
    /// </summary>
    protected static float Convert(float value) => value;

    /// <summary>
    /// Parses a string into a float value, trimming whitespace first.
    /// </summary>
    protected static float Convert(string value) => Convert(float.Parse(value.Trim()));

    /// <summary>
    /// Converts an integer to a float value.
    /// </summary>
    protected static float Convert(int value) => Convert((float)value);

    /// <summary>
    /// Converts a decimal to a float value.
    /// </summary>
    protected static float Convert(decimal value) => Convert((float)value);

    /// <summary>
    /// Converts a boolean to a float value (true = 1, false = 0).
    /// </summary>
    protected static float Convert(bool value) => Convert(value ? 1 : 0);

    /// <summary>
    /// Converts a long to a float value.
    /// </summary>
    protected static float Convert(long value) => Convert((float)value);

    /// <summary>
    /// Converts a double to a float value (may lose precision).
    /// </summary>
    protected static float Convert(double value) => Convert((float)value);

    /// <summary>
    /// Converts a <see cref="DateTime"/> value to a float representing the total seconds since its epoch (based on Ticks).
    /// </summary>
    protected static float Convert(DateTime value) => Convert((float)new TimeSpan(value.Ticks).TotalSeconds);

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated float value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}