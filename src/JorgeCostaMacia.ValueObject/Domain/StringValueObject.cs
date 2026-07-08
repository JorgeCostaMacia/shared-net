using System.Globalization;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable, generic <b>Value Object</b> that encapsulates a single <see cref="string"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for all domain Value Objects based on text strings (e.g., Email, ClientName).
/// It guarantees immutability and provides static factory methods for creation and type conversion.
/// </para>
/// <para>
/// The protected constructor is reserved for infrastructure purposes (e.g., ORM mapping, deserialization).
/// Business logic should utilize the static <c>Create</c> factory methods.
/// </para>
/// </remarks>
public record StringValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="string"/> value encapsulated by this Value Object.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// <b>Infrastructure Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation and is intended for ORMs, deserializers, and database mapping.
    /// </summary>
    /// <param name="value">The string value to encapsulate.</param>
    protected StringValueObject(string value) => Value = value;

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance from a string,
    /// applying any defined format cleansing (e.g., trimming whitespace).
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(string value) => new StringValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting an integer value (<see cref="int"/>) to a string.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(int value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a float value (<see cref="float"/>) to a string.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a decimal value (<see cref="decimal"/>) to a string.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a boolean value (<see cref="bool"/>) to a string.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(bool value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a long value (<see cref="long"/>) to a string.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(long value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a double value (<see cref="double"/>) to a string.
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(double value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a DateTime value (<see cref="DateTime"/>) to a string.
    /// </summary>
    /// <param name="value">The source DateTime value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(DateTime value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="StringValueObject"/> instance by converting a <see cref="Guid"/> to a string.
    /// </summary>
    /// <param name="value">The source Guid value.</param>
    /// <returns>A new <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject Create(Guid value) => Create(Convert(value));

    /// <summary>
    /// Converts and cleanses a string value. By default, this method trims whitespace.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The cleansed string.</returns>
    protected static string Convert(string value) => value.Trim();

    /// <summary>
    /// Converts an integer to a string.
    /// </summary>
    protected static string Convert(int value) => Convert(value.ToString());

    /// <summary>
    /// Converts a float to a string.
    /// </summary>
    protected static string Convert(float value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a decimal to a string.
    /// </summary>
    protected static string Convert(decimal value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a boolean to a string.
    /// </summary>
    protected static string Convert(bool value) => Convert(value.ToString());

    /// <summary>
    /// Converts a long to a string.
    /// </summary>
    protected static string Convert(long value) => Convert(value.ToString());

    /// <summary>
    /// Converts a double to a string.
    /// </summary>
    protected static string Convert(double value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a DateTime to a string.
    /// </summary>
    protected static string Convert(DateTime value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a Guid to a string.
    /// </summary>
    protected static string Convert(Guid value) => Convert(value.ToString());

    /// <summary>Implicitly converts the value object to its underlying <see cref="string"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator string(StringValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>).</returns>
    public override string ToString() => Value.ToString();
}
