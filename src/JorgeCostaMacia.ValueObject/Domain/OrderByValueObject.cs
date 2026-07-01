namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> specialized for storing ordering criteria (e.g., column names for a SQL ORDER BY clause).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but enforces a specific domain rule:
/// the value is always converted to <b>uppercase</b> upon creation, ensuring consistency regardless of the input case.
/// </para>
/// <para>
/// It relies on the base <see cref="StringValueObject.Convert(string)"/> for initial cleansing (trimming).
/// The validation ensures that the ordering field name adheres to required rules (e.g., valid identifier, existence).
/// </para>
/// </remarks>
public record OrderByValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers, and should contain an already normalized (uppercase) value.
    /// </summary>
    /// <param name="value">The ordering criteria string value to encapsulate.</param>
    public OrderByValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="OrderByValueObject"/> instance from a string,
    /// applying conversion and normalization logic before encapsulation.
    /// </summary>
    /// <param name="value">The source string value (e.g., "columnName").</param>
    /// <returns>A new <see cref="OrderByValueObject"/> instance with an uppercase value.</returns>
    public static new OrderByValueObject Create(string value) => new OrderByValueObject(Convert(value));

    /// <summary>
    /// Converts the input string by first applying the base <see cref="StringValueObject.Convert(string)"/>
    /// (e.g., trimming) and then converting the result entirely to uppercase.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>The normalized, uppercase string.</returns>
    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}
