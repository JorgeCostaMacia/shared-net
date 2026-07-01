namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a string value intended to hold valid JSON data.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but specializes its use to define
/// domain constraints and validation rules specific to JSON format and content.
/// </para>
/// <para>
/// It relies on the base <c>Convert</c> method (inherited from <see cref="StringValueObject"/>)
/// for initial cleansing (trimming). The structural validation of the JSON format itself is typically
/// handled by the associated FluentValidation validator.
/// </para>
/// </remarks>
public record JsonValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The JSON string value to encapsulate.</param>
    public JsonValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="JsonValueObject"/> instance from a string,
    /// applying the base class's conversion/cleansing logic before encapsulation.
    /// </summary>
    /// <param name="value">The source JSON string value.</param>
    /// <returns>A new <see cref="JsonValueObject"/> instance.</returns>
    public new static JsonValueObject Create(string value) => new JsonValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="JsonValueObject"/> instance initialized with an empty JSON object string (<c>"{}"</c>).
    /// </summary>
    /// <returns>A new instance initialized with "{}".</returns>
    public static JsonValueObject Create() => Create("{}");
}
