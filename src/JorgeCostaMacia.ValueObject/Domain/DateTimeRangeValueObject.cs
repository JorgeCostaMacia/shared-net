using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a range defined by two <see cref="DateTimeValueObject"/> instances: a start date and an end date.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that time ranges are treated as a single, cohesive unit in the domain.
/// It exposes the three-verb creation surface: the constructor hydrates from pre-built parts (ORMs,
/// deserializers), <see cref="From(DateTime, DateTime)"/> converts (materializes the parts through <b>their</b>
/// <c>From</c>, unvalidated — no part throws individually) and <see cref="Create(DateTime, DateTime)"/> fabricates
/// validated in <b>one composed pass</b> (one exception, the complete per-field failure list).
/// </para>
/// <para>
/// Both factories take the parts' natural primitive (<see cref="DateTime"/>). Other representations are
/// converted at the call site; existing <see cref="DateTimeValueObject"/> instances flow through the factories
/// via their implicit conversion to <see cref="DateTime"/>.
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
    /// <b>Hydration Constructor.</b> Assigns the pre-built parts as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping).
    /// </summary>
    /// <param name="valueStart">The start date Value Object.</param>
    /// <param name="valueEnd">The end date Value Object.</param>
    public DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Converts: materializes the whole range unvalidated, building each part through its own
    /// <see cref="DateTimeValueObject.From(DateTime)"/> — no part throws individually.
    /// </summary>
    /// <param name="valueStart">The start date value.</param>
    /// <param name="valueEnd">The end date value.</param>
    /// <returns>A new, unvalidated <see cref="DateTimeRangeValueObject"/> instance.</returns>
    public static DateTimeRangeValueObject From(DateTime valueStart, DateTime valueEnd) => new DateTimeRangeValueObject(DateTimeValueObject.From(valueStart), DateTimeValueObject.From(valueEnd));

    /// <summary>
    /// Creates: materializes the range through <see cref="From(DateTime, DateTime)"/> and validates it composed,
    /// <b>once</b> — one exception with the complete failure list (parts and range invariant together).
    /// </summary>
    /// <param name="valueStart">The start date value.</param>
    /// <param name="valueEnd">The end date value.</param>
    /// <returns>A new, validated <see cref="DateTimeRangeValueObject"/> instance.</returns>
    /// <exception cref="DateTimeRangeValueObjectValidationException">Thrown when the resulting range violates a validation rule.</exception>
    public static DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd)
    {
        DateTimeRangeValueObject vo = From(valueStart, valueEnd);
        DateTimeRangeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Returns the string representation of the date range in the format "Start Date - End Date".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
