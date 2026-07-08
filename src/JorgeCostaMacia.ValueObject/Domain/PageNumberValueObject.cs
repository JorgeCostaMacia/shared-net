namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> specialized for encapsulating a page number in a pagination context.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> but enforces domain constraints specific to pagination,
/// primarily that the value must be a positive integer (greater than or equal to 1).
/// </para>
/// <para>
/// It relies on the base <c>Convert</c> methods (inherited from <see cref="IntValueObject"/>) for type conversion.
/// </para>
/// </remarks>
public record PageNumberValueObject : IntValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The integer page number value to encapsulate.</param>
    public PageNumberValueObject(int value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="PageNumberValueObject"/> instance from an integer value.
    /// It applies conversion/cleansing logic inherited from the base class.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="PageNumberValueObject"/> instance.</returns>
    public static new PageNumberValueObject Create(int value) => new PageNumberValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="PageNumberValueObject"/> instance by converting a string representation of the number.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="PageNumberValueObject"/> instance.</returns>
    public static new PageNumberValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="PageNumberValueObject"/> instance by converting a float value.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="PageNumberValueObject"/> instance.</returns>
    public static new PageNumberValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="PageNumberValueObject"/> instance by converting a decimal value.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="PageNumberValueObject"/> instance.</returns>
    public static new PageNumberValueObject Create(decimal value) => Create(Convert(value));
}
