namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a Uniform Resource Locator (URL) string.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but specializes its use to enforce
/// domain constraints and validation rules specific to URL formats and protocols.
/// </para>
/// <para>
/// It relies on the base <c>Convert</c> method (inherited from <see cref="StringValueObject"/>)
/// for initial cleansing (trimming). The structural validation of the URL is handled by the associated FluentValidation validator.
/// </para>
/// </remarks>
public record UrlValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The URL string value to encapsulate.</param>
    public UrlValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="UrlValueObject"/> instance from a string,
    /// applying the base class's conversion/cleansing logic before encapsulation.
    /// </summary>
    /// <param name="value">The source URL string value.</param>
    /// <returns>A new <see cref="UrlValueObject"/> instance.</returns>
    public static new UrlValueObject Create(string value) => new UrlValueObject(Convert(value));
}
