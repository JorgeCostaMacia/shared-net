namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a valid email address.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> but specializes its use to enforce
/// domain constraints and rules specific to email formats (e.g., structure, maximum length, disallowed characters).
/// </para>
/// <para>
/// It relies on the <c>Convert</c> method defined in <see cref="StringValueObject"/> for initial cleansing (trimming).
/// The validation of the email format itself is handled by the associated FluentValidation validator.
/// </para>
/// </remarks>
public record EmailValueObject : StringValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The email string value to encapsulate.</param>
    public EmailValueObject(string value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="EmailValueObject"/> instance from a string,
    /// applying the base class's conversion/cleansing logic before encapsulation.
    /// </summary>
    /// <param name="value">The source email string value.</param>
    /// <returns>A new <see cref="EmailValueObject"/> instance.</returns>
    public new static EmailValueObject Create(string value) => new EmailValueObject(Convert(value));
}
