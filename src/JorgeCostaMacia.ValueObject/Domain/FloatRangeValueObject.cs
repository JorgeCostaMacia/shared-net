using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a numeric range defined by two <see cref="FloatValueObject"/> instances: a start value and an end value.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that numeric ranges are treated as a single, cohesive unit in the domain.
/// It exposes the three-verb creation surface: the constructor hydrates from pre-built parts (ORMs,
/// deserializers), <see cref="From(float, float)"/> converts (materializes the parts through <b>their</b>
/// <c>From</c>, unvalidated — no part throws individually) and <see cref="Create(float, float)"/> fabricates
/// validated in <b>one composed pass</b> (one exception, the complete per-field failure list).
/// </para>
/// <para>
/// Both factories take the parts' natural primitive (<see cref="float"/>). Other primitive types are
/// converted at the call site; existing <see cref="FloatValueObject"/> instances flow through the factories
/// via their implicit conversion to <see cref="float"/>.
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
    /// <b>Hydration Constructor.</b> Assigns the pre-built parts as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping).
    /// </summary>
    /// <param name="valueStart">The start numeric Value Object.</param>
    /// <param name="valueEnd">The end numeric Value Object.</param>
    public FloatRangeValueObject(FloatValueObject valueStart, FloatValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Converts: materializes the whole range unvalidated, building each part through its own
    /// <see cref="FloatValueObject.From(float)"/> — no part throws individually.
    /// </summary>
    /// <param name="valueStart">The start float value.</param>
    /// <param name="valueEnd">The end float value.</param>
    /// <returns>A new, unvalidated <see cref="FloatRangeValueObject"/> instance.</returns>
    public static FloatRangeValueObject From(float valueStart, float valueEnd) => new FloatRangeValueObject(FloatValueObject.From(valueStart), FloatValueObject.From(valueEnd));

    /// <summary>
    /// Creates: materializes the range through <see cref="From(float, float)"/> and validates it composed,
    /// <b>once</b> — one exception with the complete failure list (parts and range invariant together).
    /// </summary>
    /// <param name="valueStart">The start float value.</param>
    /// <param name="valueEnd">The end float value.</param>
    /// <returns>A new, validated <see cref="FloatRangeValueObject"/> instance.</returns>
    /// <exception cref="FloatRangeValueObjectValidationException">Thrown when the resulting range violates a validation rule.</exception>
    public static FloatRangeValueObject Create(float valueStart, float valueEnd)
    {
        FloatRangeValueObject vo = From(valueStart, valueEnd);
        FloatRangeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Returns the string representation of the numeric range in the format "Start Value - End Value".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
