namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a range defined by two <see cref="DateTimeValueObject"/> instances: a start date and an end date.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that time ranges are treated as a single, cohesive unit in the domain.
/// It relies on <see cref="DateTimeValueObject"/> for its internal structure, inheriting its immutability and conversion logic.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and robust type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record DateTimeRangeValueObject : IValueObject
{
    /// <summary>
    /// The start date of the range, encapsulated as a <see cref="DateTimeValueObject"/>.
    /// </summary>
    public DateTimeValueObject ValueStart { get; init; }

    /// <summary>
    /// The end date of the range, encapsulated as a <see cref="DateTimeValueObject"/>.
    /// </summary>
    public DateTimeValueObject ValueEnd { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object with pre-validated start and end date Value Objects.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended
    /// to ensure internal consistency and proper type conversion.
    /// </summary>
    /// <param name="valueStart">The start date Value Object.</param>
    /// <param name="valueEnd">The end date Value Object.</param>
    public DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance from existing <see cref="DateTimeValueObject"/> instances.
    /// </summary>
    /// <param name="valueStart">The start date Value Object.</param>
    /// <param name="valueEnd">The end date Value Object.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(DateTimeValueObject valueStart, DateTimeValueObject valueEnd) => new DateTimeRangeValueObject(valueStart, valueEnd);

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance from standard <see cref="DateTime"/> values.
    /// </summary>
    /// <param name="valueStart">The start date.</param>
    /// <param name="valueEnd">The end date.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance by converting string representations of the dates.
    /// </summary>
    /// <param name="valueStart">The start date string.</param>
    /// <param name="valueEnd">The end date string.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(string valueStart, string valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance by converting integer representations of the dates.
    /// </summary>
    /// <param name="valueStart">The start date integer.</param>
    /// <param name="valueEnd">The end date integer.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(int valueStart, int valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance by converting float representations of the dates.
    /// </summary>
    /// <param name="valueStart">The start date float.</param>
    /// <param name="valueEnd">The end date float.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(float valueStart, float valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="DateTimeRangeValueObject"/> instance by converting decimal representations of the dates.
    /// </summary>
    /// <param name="valueStart">The start date decimal.</param>
    /// <param name="valueEnd">The end date decimal.</param>
    /// <returns>A new <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    /// <summary>
    /// Generates the hash code based on the combined hash of <see cref="ValueStart"/> and <see cref="ValueEnd"/>.
    /// </summary>
    /// <returns>The combined hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);

    /// <summary>
    /// Returns the string representation of the date range in the format "Start Date - End Date".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
