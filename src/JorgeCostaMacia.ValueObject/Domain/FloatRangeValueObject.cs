namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a numeric range defined by two <see cref="FloatValueObject"/> instances: a start value and an end value.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that numeric ranges are treated as a single, cohesive unit in the domain.
/// It relies on <see cref="FloatValueObject"/> for its internal structure, inheriting its immutability and conversion logic.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and robust type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record FloatRangeValueObject : IValueObject
{
    /// <summary>
    /// The start value of the range, encapsulated as a <see cref="FloatValueObject"/>.
    /// </summary>
    public FloatValueObject ValueStart { get; init; }

    /// <summary>
    /// The end value of the range, encapsulated as a <see cref="FloatValueObject"/>.
    /// </summary>
    public FloatValueObject ValueEnd { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object with pre-validated start and end numeric Value Objects.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="valueStart">The start numeric Value Object.</param>
    /// <param name="valueEnd">The end numeric Value Object.</param>
    public FloatRangeValueObject(FloatValueObject valueStart, FloatValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Creates a new <see cref="FloatRangeValueObject"/> instance from existing <see cref="FloatValueObject"/> instances.
    /// </summary>
    /// <param name="valueStart">The start numeric Value Object.</param>
    /// <param name="valueEnd">The end numeric Value Object.</param>
    /// <returns>A new <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject Create(FloatValueObject valueStart, FloatValueObject valueEnd) => new FloatRangeValueObject(valueStart, valueEnd);

    /// <summary>
    /// Creates a new <see cref="FloatRangeValueObject"/> instance from standard <see cref="float"/> values.
    /// </summary>
    /// <param name="valueStart">The start float value.</param>
    /// <param name="valueEnd">The end float value.</param>
    /// <returns>A new <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject Create(float valueStart, float valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="FloatRangeValueObject"/> instance by converting string representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start float string.</param>
    /// <param name="valueEnd">The end float string.</param>
    /// <returns>A new <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject Create(string valueStart, string valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="FloatRangeValueObject"/> instance by converting integer representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start integer value.</param>
    /// <param name="valueEnd">The end integer value.</param>
    /// <returns>A new <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject Create(int valueStart, int valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));

    /// <summary>
    /// Creates a new <see cref="FloatRangeValueObject"/> instance by converting decimal representations of the numbers.
    /// </summary>
    /// <param name="valueStart">The start decimal value.</param>
    /// <param name="valueEnd">The end decimal value.</param>
    /// <returns>A new <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));

    /// <summary>
    /// Generates the hash code based on the combined hash of the encapsulated <see cref="ValueStart"/> and <see cref="ValueEnd"/> float primitives.
    /// </summary>
    /// <returns>The combined hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);

    /// <summary>
    /// Returns the string representation of the numeric range in the format "Start Value - End Value".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
