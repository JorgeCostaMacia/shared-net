using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an array of bytes (<see cref="byte"/>[]).
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects that handle binary data (e.g., FileContent, Image, Hash/Salt).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(byte[])"/> converts (materializes, unvalidated) and
/// <see cref="Create(byte[])"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="byte"/>[]). The protected <c>Convert</c>
/// family carries the conversion logic from other representations (notably Base64 strings), so Value
/// Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record ByteValueObject : IValueObject
{
    /// <summary>
    /// The immutable array of bytes (<see cref="byte"/>[]) encapsulated by this Value Object.
    /// </summary>
    public byte[] Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The byte array to encapsulate.</param>
    public ByteValueObject(byte[] value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="ByteValueObject"/> from the natural primitive through
    /// <see cref="Convert(byte[])"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source byte array.</param>
    /// <returns>A new, unvalidated <see cref="ByteValueObject"/> instance.</returns>
    public static ByteValueObject From(byte[] value) => new ByteValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(byte[])"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source byte array.</param>
    /// <returns>A new, validated <see cref="ByteValueObject"/> instance.</returns>
    /// <exception cref="ByteValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static ByteValueObject Create(byte[] value)
    {
        ByteValueObject vo = From(value);
        ByteValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts an existing byte array (identity conversion).
    /// </summary>
    protected static byte[] Convert(byte[] value) => value;

    /// <summary>
    /// Converts a Base64 string to a byte array, trimming whitespace before conversion.
    /// </summary>
    /// <param name="value">The Base64 string to convert.</param>
    /// <returns>The resulting byte array.</returns>
    /// <exception cref="FormatException">Thrown if the string is not a valid Base64 string.</exception>
    protected static byte[] Convert(string value) => Convert(System.Convert.FromBase64String(value.Trim()));

    /// <summary>Implicitly converts the value object to its underlying <see cref="byte"/>[] value.</summary>
    /// <remarks>
    /// Unlike the scalar value objects, this one wraps a <see cref="byte"/>[] (a binary blob), not a single scalar.
    /// The operator is kept so the raw bytes can be used directly — e.g. binary queries/comparisons against the
    /// database column. Two caveats follow from the array type (they already applied to the public <c>Value</c>,
    /// the operator does not make them worse): the returned reference is the <b>underlying array</b> — treat it as
    /// read-only, do not mutate it; and equality is by reference (records compare arrays by reference), so two
    /// instances with the same bytes are not equal. If a fully value-semantic byte value object is ever needed,
    /// give it structural equality (content <c>SequenceEqual</c> + a content-based hash) as its own change.
    /// </remarks>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator byte[](ByteValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns a string representation of the byte array, encoded using UTF8.
    /// Note: This operation may be lossy or result in unreadable characters if the bytes do not represent valid UTF8 text.
    /// </summary>
    /// <returns>The internal byte array (<see cref="Value"/>) decoded as a UTF8 string.</returns>
    public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);
}
