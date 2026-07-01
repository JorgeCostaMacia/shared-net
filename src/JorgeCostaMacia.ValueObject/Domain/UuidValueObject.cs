namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="Guid"/> (UUID) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on GUID/UUID logic (e.g., entity IDs, correlation IDs).
/// It guarantees immutability and provides robust static factory methods for conversion from primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safety, the static <c>Create</c> factory methods are highly recommended.
/// </para>
/// </remarks>
public record UuidValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="Guid"/> value encapsulated by this Value Object.
    /// </summary>
    public Guid Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. While public, using the static <c>Create</c> methods is highly recommended
    /// to ensure type conversion and adherence to best practices.
    /// </summary>
    /// <param name="value">The GUID value to encapsulate.</param>
    public UuidValueObject(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="UuidValueObject"/> instance from an existing GUID value (identity conversion).
    /// </summary>
    /// <param name="value">The source GUID value.</param>
    /// <returns>A new <see cref="UuidValueObject"/> instance.</returns>
    public static UuidValueObject Create(Guid value) => new UuidValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="UuidValueObject"/> instance by converting a string representation to a GUID.
    /// </summary>
    /// <param name="value">The source string value (must be a valid GUID format).</param>
    /// <returns>A new <see cref="UuidValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a GUID.</exception>
    public static UuidValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Converts a GUID value (identity conversion).
    /// </summary>
    protected static Guid Convert(Guid value) => value;

    /// <summary>
    /// Converts a string to a GUID value.
    /// </summary>
    protected static Guid Convert(string value) => Convert(new Guid(value.Trim()));

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated GUID value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a standard string representation of a GUID.</returns>
    public override string ToString() => Value.ToString();
}
