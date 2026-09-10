using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a "group by" criterion (e.g., a field name) for queries.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(string)"/> converts
/// (normalizes through <see cref="Convert(string)"/>: trims and upper-cases, unvalidated) and
/// <see cref="Create(string)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>). The protected <c>Convert</c>
/// family remains available for Value Objects deriving from this one in consuming contexts.
/// </para>
/// </remarks>
public record GroupByValueObject : StringValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing normalization and validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The group-by string value to encapsulate.</param>
    public GroupByValueObject(string value) : base(value) { }

    /// <summary>
    /// Converts: normalizes the input through <see cref="Convert(string)"/> (trims whitespace and converts to
    /// uppercase) and materializes a new <see cref="GroupByValueObject"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source group-by string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="GroupByValueObject"/> instance.</returns>
    public static new GroupByValueObject From(string value) => new GroupByValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(string)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The group-by string value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="GroupByValueObject"/> instance, or <see langword="null"/>.</returns>
    public static new GroupByValueObject? FromOrNull(string? value) => value is null ? null : From(value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source group-by string value.</param>
    /// <returns>A new, validated <see cref="GroupByValueObject"/> instance.</returns>
    /// <exception cref="GroupByValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new GroupByValueObject Create(string value)
    {
        GroupByValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(string)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The group-by string value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="GroupByValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="GroupByValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static new GroupByValueObject? CreateOrNull(string? value)
    {
        GroupByValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => GroupByValueObjectValidator.Create().ValidateAndThrow(this);

    /// <summary>
    /// Converts the input string by first applying the base <see cref="StringValueObject.Convert(string)"/>
    /// cleansing (trimming) and then converting the result to uppercase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The cleansed, uppercase string.</returns>
    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}
