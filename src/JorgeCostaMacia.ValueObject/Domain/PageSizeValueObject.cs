namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> specialized for encapsulating the page size (number of items per page) in a pagination context.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> but enforces domain constraints specific to pagination limits,
/// primarily that the value must be a positive integer, usually with an upper bound defined by the domain.
/// </para>
/// <para>
/// It relies on the base <c>Convert</c> methods (inherited from <see cref="IntValueObject"/>) for type conversion.
/// The default creation value is set to <b>10000</b>, serving as a domain-defined maximum default size.
/// </para>
/// </remarks>
public record PageSizeValueObject : IntValueObject
{
    /// <summary>
    /// <b>Primary Constructor (Infrastructure).</b> Initializes the Value Object using the base class constructor.
    /// This is typically reserved for ORMs and deserializers.
    /// </summary>
    /// <param name="value">The integer page size value to encapsulate.</param>
    public PageSizeValueObject(int value) : base(value) { }

    /// <summary>
    /// Creates a new <see cref="PageSizeValueObject"/> instance from an integer value.
    /// It applies conversion/cleansing logic inherited from the base class.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject Create(int value) => new PageSizeValueObject(Convert(value));

    /// <summary>Creates a new <see cref="PageSizeValueObject"/> initialized with the default page size (10000).</summary>
    public static PageSizeValueObject Create() => Create(10000);

    /// <summary>
    /// Creates a new <see cref="PageSizeValueObject"/> instance by converting a string representation of the number.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="PageSizeValueObject"/> instance by converting a float value.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="PageSizeValueObject"/> instance by converting a decimal value.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject Create(decimal value) => Create(Convert(value));
}
