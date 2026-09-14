using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a page size for pagination (a positive integer).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(int)"/> converts
/// (materializes, unvalidated) and <see cref="Create(int)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="int"/>) and convert through the inherited
/// <c>Convert</c> family. The positivity constraint is enforced by <see cref="PageSizeValueObjectValidator"/>.
/// </para>
/// </remarks>
public record PageSizeValueObject : IntValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The page size value to encapsulate.</param>
    public PageSizeValueObject(int value) : base(value) { }

    /// <summary>
    /// Converts: materializes a new <see cref="PageSizeValueObject"/> from the natural primitive through
    /// the base <see cref="IntValueObject.Convert(int)"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source page size value.</param>
    /// <returns>A new, unvalidated <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject From(int value) => new PageSizeValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(int)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The page size value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="PageSizeValueObject"/> instance, or <see langword="null"/>.</returns>
    public static new PageSizeValueObject? FromOrNull(int? value) => value is null ? null : From(value.Value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(int)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source page size value.</param>
    /// <returns>A new, validated <see cref="PageSizeValueObject"/> instance.</returns>
    /// <exception cref="PageSizeValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new PageSizeValueObject Create(int value)
    {
        PageSizeValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(int)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The page size value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="PageSizeValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="PageSizeValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static new PageSizeValueObject? CreateOrNull(int? value)
    {
        PageSizeValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>
    /// How many pages a row count splits into at this page size — the ceiling division, in one place so
    /// the last, partial page cannot be lost to an integer division written from memory.
    /// </summary>
    /// <param name="count">The number of rows to split. Zero or less is zero pages.</param>
    /// <returns>The number of pages the count occupies at this size.</returns>
    public int Pages(int count) => count <= 0 ? 0 : (count + Value - 1) / Value;

    /// <summary>
    /// How many rows to skip to reach a page at this size — the other direction of <see cref="Pages(int)"/>,
    /// kept beside it because the two conversions between rows and pages are the two places the arithmetic
    /// goes wrong: the division that loses the last page, and the offset that misses it by one whole page.
    /// </summary>
    /// <param name="pageNumber">The 1-based page to reach. The first page skips nothing.</param>
    /// <returns>The number of rows preceding that page at this size.</returns>
    public int Offset(int pageNumber) => (pageNumber - 1) * Value;

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => PageSizeValueObjectValidator.Create().ValidateAndThrow(this);
}
