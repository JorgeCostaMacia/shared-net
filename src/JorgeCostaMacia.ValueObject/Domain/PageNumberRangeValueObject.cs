using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a range of page numbers defined by two <see cref="PageNumberValueObject"/> instances: a start page and an end page.
/// </summary>
/// <remarks>
/// <para>
/// This class ensures that page number ranges are treated as a single, cohesive unit in the domain.
/// It exposes the three-verb creation surface: the constructor hydrates from pre-built parts (ORMs,
/// deserializers), <see cref="From(int, int)"/> converts (materializes the parts through <b>their</b>
/// <c>From</c>, unvalidated — no part throws individually) and <see cref="Create(int, int)"/> fabricates
/// validated in <b>one composed pass</b> (one exception, the complete per-field failure list).
/// </para>
/// <para>
/// Both factories take the parts' natural primitive (<see cref="int"/>). Other primitive types are
/// converted at the call site; existing <see cref="PageNumberValueObject"/> instances flow through the
/// factories via their implicit conversion to <see cref="int"/>.
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
    /// <b>Hydration Constructor.</b> Assigns the pre-built parts as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping).
    /// </summary>
    /// <param name="valueStart">The start page number Value Object.</param>
    /// <param name="valueEnd">The end page number Value Object.</param>
    public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    /// <summary>
    /// Converts: materializes the whole range unvalidated, building each part through its own
    /// <see cref="PageNumberValueObject.From(int)"/> — no part throws individually.
    /// </summary>
    /// <param name="valueStart">The start page number value.</param>
    /// <param name="valueEnd">The end page number value.</param>
    /// <returns>A new, unvalidated <see cref="PageNumberRangeValueObject"/> instance.</returns>
    public static PageNumberRangeValueObject From(int valueStart, int valueEnd) => new PageNumberRangeValueObject(PageNumberValueObject.From(valueStart), PageNumberValueObject.From(valueEnd));

    /// <summary>
    /// Creates: materializes the range through <see cref="From(int, int)"/> and validates it composed,
    /// <b>once</b> — one exception with the complete failure list (parts and range invariant together).
    /// </summary>
    /// <param name="valueStart">The start page number value.</param>
    /// <param name="valueEnd">The end page number value.</param>
    /// <returns>A new, validated <see cref="PageNumberRangeValueObject"/> instance.</returns>
    /// <exception cref="PageNumberRangeValueObjectValidationException">Thrown when the resulting range violates a validation rule.</exception>
    public static PageNumberRangeValueObject Create(int valueStart, int valueEnd)
    {
        PageNumberRangeValueObject vo = From(valueStart, valueEnd);
        PageNumberRangeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Returns the string representation of the page number range in the format "Start Page - End Page".
    /// </summary>
    /// <returns>The combined string representation of the range.</returns>
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
