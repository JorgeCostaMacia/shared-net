using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="Uri"/>, relative or absolute.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects that hold a URI (e.g., notification endpoints, callback
/// URLs, resource locators). It exposes the three-verb creation surface: the constructor hydrates (raw assignment for
/// ORMs and deserializers), <see cref="From(Uri)"/> converts (materializes, unvalidated) and <see cref="Create(Uri)"/>
/// fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// The base rule is deliberately general — the URI must be present, relative or absolute, whatever its scheme. A
/// Value Object that needs more (absolute, only <c>http</c>/<c>https</c>, a given host) derives from this one and adds the rule in its own validator,
/// the same way a module's string Value Object adds its format to <see cref="StringValueObject"/>.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="Uri"/>). The protected <c>Convert</c> family carries
/// the conversion from text, so Value Objects deriving from this one in consuming contexts can reuse and redefine it.
/// The text the URI was built from is kept: <see cref="ToString"/> returns <see cref="Uri.OriginalString"/>, not the
/// normalized form, so a value reads back as it arrived.
/// </para>
/// </remarks>
public record UriValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="Uri"/> value encapsulated by this Value Object.
    /// </summary>
    public Uri Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The URI value to encapsulate.</param>
    public UriValueObject(Uri value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="UriValueObject"/> from the natural primitive through
    /// <see cref="Convert(Uri)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source URI value.</param>
    /// <returns>A new, unvalidated <see cref="UriValueObject"/> instance.</returns>
    public static UriValueObject From(Uri value) => new UriValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(Uri)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The URI value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="UriValueObject"/> instance, or <see langword="null"/>.</returns>
    public static UriValueObject? FromOrNull(Uri? value) => value is null ? null : From(value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(Uri)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source URI value.</param>
    /// <returns>A new, validated <see cref="UriValueObject"/> instance.</returns>
    /// <exception cref="UriValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static UriValueObject Create(Uri value)
    {
        UriValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(Uri)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The URI value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="UriValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="UriValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static UriValueObject? CreateOrNull(Uri? value)
    {
        UriValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => UriValueObjectValidator.Create().ValidateAndThrow(this);

    /// <summary>
    /// Converts a URI value (identity conversion).
    /// </summary>
    protected static Uri Convert(Uri value) => value;

    /// <summary>
    /// Converts a string to a URI value. Text that is not a URI throws <see cref="UriFormatException"/>; relative and
    /// absolute URIs both convert.
    /// </summary>
    protected static Uri Convert(string value) => Convert(new Uri(value.Trim(), UriKind.RelativeOrAbsolute));

    /// <summary>Implicitly converts the value object to its underlying <see cref="Uri"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator Uri(UriValueObject valueObject) => valueObject.Value;

    /// <summary>Returns the text the URI was built from, as it arrived (<see cref="Uri.OriginalString"/>).</summary>
    /// <returns>The original URI text.</returns>
    public sealed override string ToString() => Value.OriginalString;
}
