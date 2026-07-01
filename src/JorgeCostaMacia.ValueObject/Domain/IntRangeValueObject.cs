namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an integer range defined by two <see cref="IntValueObject"/> instances: a start value and an end value.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that integer ranges are treated as a single, cohesive unit in the domain.
/// It relies on <see cref="IntValueObject"/> for its internal structure, inheriting its immutability and conversion logic.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and robust type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record IntRangeValueObject : IValueObject
{
    /// <summary>
    /// The start value of the range, encapsulated as an <see cref="IntValueObject"/>.
    /// </summary>
    public IntValueObject ValueStart { get; init; }

    /// <summary>
    /// The end value of the range, encapsulated as an <see cref="IntValueObject"/>.
    /// </summary>
    public IntValueObject ValueEnd { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object with pre-validated start and end integer Value Objects.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="valueStart">The start integer Value Object.</param>
    /// <param name="valueEnd">The end integer Value Object.</param>
    public IntRangeValueObject(IntValueObject valueStart, IntValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Creates a new <see cref="IntRangeValueObject"/> instance from existing <see cref="IntValueObject"/> instances.
    /// </summary>
    /// <param name="valueStart">The start integer Value Object.</param>
    /// <param name="valueEnd">The end integer Value Object.</param>
    /// <returns>A new <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject Create(IntValueObject valueStart, IntValueObject valueEnd) => new IntRangeValueObject(valueStart, valueEnd);

    /// <summary>
    /// Creates a new <see cref="IntRangeValueObject"/> instance from standard <see cref="int"/> values.
    /// </summary>
    /// <param name="valueStart">The start integer value.</param>
    /// <param name="valueEnd">The end integer value.</param>
    /// <returns>A new <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject Create(int valueStart, int valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="IntRangeValueObject"/> instance by converting string representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start integer string.</param>
    /// <param name="valueEnd">The end integer string.</param>
    /// <returns>A new <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject Create(string valueStart, string valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="IntRangeValueObject"/> instance by converting float representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start float value.</param>
    /// <param name="valueEnd">The end float value.</param>
    /// <returns>A new <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject Create(float valueStart, float valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="IntRangeValueObject"/> instance by converting decimal representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start decimal value.</param>
    /// <param name="valueEnd">The end decimal value.</param>
    /// <returns>A new <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));

    /// <summary>
    /// Generates the hash code based on the combined hash of the encapsulated <see cref="ValueStart"/> and <see cref="ValueEnd"/> integer primitives.
    /// </summary>
    /// <returns>The combined hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);

    /// <summary>
    /// Returns the string representation of the integer range in the format "Start Value - End Value".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
