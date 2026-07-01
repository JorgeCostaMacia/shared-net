namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an array of bytes (<see cref="byte"/>[]).
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects that handle binary data (e.g., FileContent, Image, Hash/Salt).
/// It guarantees immutability and provides static factory methods for creation and conversion, notably from Base64 strings.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record ByteValueObject : IValueObject
{
    /// <summary>
    /// The immutable array of bytes (<see cref="byte"/>[]) encapsulated by this Value Object.
    /// </summary>
    public byte[] Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic and is primarily intended for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The byte array to encapsulate.</param>
    public ByteValueObject(byte[] value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="ByteValueObject"/> instance from an existing byte array.
    /// </summary>
    /// <param name="value">The source byte array.</param>
    /// <returns>A new <see cref="ByteValueObject"/> instance.</returns>
    public static ByteValueObject Create(byte[] value) => new ByteValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="ByteValueObject"/> instance by converting a Base64 string to a byte array.
    /// </summary>
    /// <param name="value">The source Base64 string.</param>
    /// <returns>A new <see cref="ByteValueObject"/> instance.</returns>
    public static ByteValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Converts an existing byte array (identity conversion).
    /// </summary>
    protected static byte[] Convert(byte[] value) => value;

    /// <summary>
    /// Converts a Base64 string to a byte array, trimming whitespace before conversion.
    /// </summary>
    /// <param name="value">The Base64 string to convert.</param>
    /// <returns>The resulting byte array.</returns>
    /// <exception cref="FormatException">Thrown if the string is not a valid Base64 string.</exception>
    protected static byte[] Convert(string value) => Convert(System.Convert.FromBase64String(value.Trim()));

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns a string representation of the byte array, encoded using UTF8.
    /// Note: This operation may be lossy or result in unreadable characters if the bytes do not represent valid UTF8 text.
    /// </summary>
    /// <returns>The internal byte array (<see cref="Value"/>) decoded as a UTF8 string.</returns>
    public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);
}
