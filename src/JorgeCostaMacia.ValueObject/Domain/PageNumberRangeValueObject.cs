namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a range of page numbers defined by two <see cref="PageNumberValueObject"/> instances: a start page and an end page.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that page number ranges are treated as a single, cohesive unit in the domain.
/// It relies on <see cref="PageNumberValueObject"/> for its internal structure, inheriting its immutability and focused validation.
/// </para>
/// <para>
/// The default range is typically set to the first page (1 to 1). The validation (handled by the associated validator)
/// must ensure the range invariant: the start page number must be less than or equal to the end page number.
/// </para>
/// </remarks>
public record PageNumberRangeValueObject : IValueObject
{
    /// <summary>
    /// The start page number of the range, encapsulated as a <see cref="PageNumberValueObject"/>.
    /// </summary>
    public PageNumberValueObject ValueStart { get; init; }

    /// <summary>
    /// The end page number of the range, encapsulated as a <see cref="PageNumberValueObject"/>.
    /// </summary>
    public PageNumberValueObject ValueEnd { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object with pre-validated start and end page Value Objects.
    /// </summary>
    /// <param name="valueStart">The start page number Value Object.</param>
    /// <param name="valueEnd">The end page number Value Object.</param>
    public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Creates a new <see cref="PageNumberRangeValueObject"/> instance from existing <see cref="PageNumberValueObject"/> instances.
    /// </summary>
    /// <param name="valueStart">The start page Value Object.</param>
    /// <param name="valueEnd">The end page Value Object.</param>
    /// <returns>A new <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject Create(PageNumberValueObject valueStart, PageNumberValueObject valueEnd) => new PageNumberRangeValueObject(valueStart, valueEnd);

    /// <summary>
    /// Creates a new <see cref="PageNumberRangeValueObject"/> instance from standard <see cref="int"/> values.
    /// </summary>
    /// <param name="valueStart">The start integer page number.</param>
    /// <param name="valueEnd">The end integer page number.</param>
    /// <returns>A new <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject Create(int valueStart, int valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="PageNumberRangeValueObject"/> instance by converting string representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start page number string.</param>
    /// <param name="valueEnd">The end page number string.</param>
    /// <returns>A new <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject Create(string valueStart, string valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="PageNumberRangeValueObject"/> instance by converting float representations of the numbers (which should typically be integers for page numbers).
    /// </summary>
    /// <param name="valueStart">The start float value.</param>
    /// <param name="valueEnd">The end float value.</param>
    /// <returns>A new <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject Create(float valueStart, float valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="PageNumberRangeValueObject"/> instance by converting decimal representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start decimal value.</param>
    /// <param name="valueEnd">The end decimal value.</param>
    /// <returns>A new <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));

    /// <summary>
    /// Returns the string representation of the page number range in the format "Start Page - End Page".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
