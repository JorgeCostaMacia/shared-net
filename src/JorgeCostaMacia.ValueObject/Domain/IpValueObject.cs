namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an IP address string (either IPv4 or IPv6).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but specializes its use to enforce
/// domain constraints and validation rules specific to IP address formats.
/// </para>
/// <para>
/// It relies on the base <c>Convert</c> method (inherited from <see cref="StringValueObject"/>)
/// for initial cleansing (trimming). The validation of the IP format itself is handled by the associated FluentValidation validator.
/// </para>
/// </remarks>
public record IpValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The IP address string value to encapsulate.</param>
    public IpValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="IpValueObject"/> instance from a string,
    /// applying the base class's conversion/cleansing logic before encapsulation.
    /// </summary>
    /// <param name="value">The source IP address string value.</param>
    /// <returns>A new <see cref="IpValueObject"/> instance.</returns>
    public new static IpValueObject Create(string value) => new IpValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="IpValueObject"/> instance initialized with the default IP address (<c>0.0.0.0</c>).
    /// </summary>
    /// <returns>A new instance initialized with "0.0.0.0".</returns>
    public static IpValueObject Create() => Create("0.0.0.0");
}
