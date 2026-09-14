using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a page number for pagination (a positive integer).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(int)"/> converts
/// (materializes, unvalidated) and <see cref="Create(int)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="int"/>) and convert through the inherited
/// <c>Convert</c> family. The positivity constraint is enforced by <see cref="PageNumberValueObjectValidator"/>.
/// </para>
/// </remarks>
public record PageNumberValueObject : IntValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The page number value to encapsulate.</param>
    public PageNumberValueObject(int value) : base(value) { }

    /// <summary>
    /// Converts: materializes a new <see cref="PageNumberValueObject"/> from the natural primitive through
    /// the base <see cref="IntValueObject.Convert(int)"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source page number value.</param>
    /// <returns>A new, unvalidated <see cref="PageNumberValueObject"/> instance.</returns>
    public static new PageNumberValueObject From(int value) => new PageNumberValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(int)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The page number value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="PageNumberValueObject"/> instance, or <see langword="null"/>.</returns>
    public static new PageNumberValueObject? FromOrNull(int? value) => value is null ? null : From(value.Value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(int)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source page number value.</param>
    /// <returns>A new, validated <see cref="PageNumberValueObject"/> instance.</returns>
    /// <exception cref="PageNumberValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new PageNumberValueObject Create(int value)
    {
        PageNumberValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(int)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The page number value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="PageNumberValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="PageNumberValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static new PageNumberValueObject? CreateOrNull(int? value)
    {
        PageNumberValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>
    /// This page, brought within the pages that exist: one that reached past the last becomes the last.
    /// A step of its own on purpose — the factories keep taking the number the caller asked for, and the
    /// bounding reads where it happens.
    /// </summary>
    /// <remarks>
    /// What it bounds against is runtime state — how many pages the source holds — not a rule of this
    /// type, which is why it is a method and not a factory.
    /// </remarks>
    /// <param name="pages">How many pages the source holds (see <see cref="PageSizeValueObject.Pages(int)"/>).</param>
    /// <returns>This page, or the last one that exists when this reached past it.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown below one page. There is no page to bound to, and answering zero would hand back something that is not a page at all.</exception>
    public int Within(int pages)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pages, 1);

        return Math.Min(Value, pages);
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => PageNumberValueObjectValidator.Create().ValidateAndThrow(this);
}
