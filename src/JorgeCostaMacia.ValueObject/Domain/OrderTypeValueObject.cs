namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> specialized for storing the order type for sorting operations (e.g., ASCENDING or DESCENDING).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but enforces a specific domain rule:
/// the value is always converted to <b>uppercase</b> upon creation, ensuring consistency regardless of the input case.
/// </para>
/// <para>
/// The default creation value is set to "ASC" (Ascending).
/// The validation ensures that the value is one of the permitted order types (e.g., "ASC" or "DESC").
/// </para>
/// </remarks>
public record OrderTypeValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers, and should contain an already normalized (uppercase) value.
    /// </summary>
    /// <param name="value">The order type string value (e.g., "ASC" or "DESC").</param>
    public OrderTypeValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="OrderTypeValueObject"/> instance from a string,
    /// applying conversion and normalization logic before encapsulation.
    /// </summary>
    /// <param name="value">The source string value (e.g., "asc", "Desc").</param>
    /// <returns>A new <see cref="OrderTypeValueObject"/> instance with an uppercase value.</returns>
    public new static OrderTypeValueObject Create(string value) => new OrderTypeValueObject(Convert(value));

    /// <summary>
    /// Converts the input string by first applying the base <see cref="StringValueObject.Convert(string)"/>
    /// (e.g., trimming) and then converting the result entirely to uppercase.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>The normalized, uppercase string.</returns>
    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}
