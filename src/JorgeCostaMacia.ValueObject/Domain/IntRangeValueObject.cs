using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an integer range defined by two <see cref="IntValueObject"/> instances: a start value and an end value.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that integer ranges are treated as a single, cohesive unit in the domain.
/// It exposes the three-verb creation surface: the constructor hydrates from pre-built parts (ORMs,
/// deserializers), <see cref="From(int, int)"/> converts (materializes the parts through <b>their</b>
/// <c>From</c>, unvalidated — no part throws individually) and <see cref="Create(int, int)"/> fabricates
/// validated in <b>one composed pass</b> (one exception, the complete per-field failure list).
/// </para>
/// <para>
/// Both factories take the parts' natural primitive (<see cref="int"/>). Other primitive types are
/// converted at the call site; existing <see cref="IntValueObject"/> instances flow through the factories
/// via their implicit conversion to <see cref="int"/>.
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
    /// <b>Hydration Constructor.</b> Assigns the pre-built parts as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping).
    /// </summary>
    /// <param name="valueStart">The start integer Value Object.</param>
    /// <param name="valueEnd">The end integer Value Object.</param>
    public IntRangeValueObject(IntValueObject valueStart, IntValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Converts: materializes the whole range unvalidated, building each part through its own
    /// <see cref="IntValueObject.From(int)"/> — no part throws individually.
    /// </summary>
    /// <param name="valueStart">The start integer value.</param>
    /// <param name="valueEnd">The end integer value.</param>
    /// <returns>A new, unvalidated <see cref="IntRangeValueObject"/> instance.</returns>
    public static IntRangeValueObject From(int valueStart, int valueEnd) => new IntRangeValueObject(IntValueObject.From(valueStart), IntValueObject.From(valueEnd));

    /// <summary>
    /// Creates: materializes the range through <see cref="From(int, int)"/> and validates it composed,
    /// <b>once</b> — one exception with the complete failure list (parts and range invariant together).
    /// </summary>
    /// <param name="valueStart">The start integer value.</param>
    /// <param name="valueEnd">The end integer value.</param>
    /// <returns>A new, validated <see cref="IntRangeValueObject"/> instance.</returns>
    /// <exception cref="IntRangeValueObjectValidationException">Thrown when the resulting range violates a validation rule.</exception>
    public static IntRangeValueObject Create(int valueStart, int valueEnd)
    {
        IntRangeValueObject vo = From(valueStart, valueEnd);
        IntRangeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Returns the string representation of the integer range in the format "Start Value - End Value".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
