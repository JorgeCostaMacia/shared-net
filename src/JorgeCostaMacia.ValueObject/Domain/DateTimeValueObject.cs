namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="DateTime"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on time points (e.g., CreatedAt, EventDate).
/// It guarantees immutability and <b>preserves the supplied value's <see cref="DateTimeKind"/> as-is</b> — it does
/// not convert the timezone. Use a dedicated UTC value object when you need a UTC guarantee.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safe type conversion, the static <c>Create</c> factory methods are preferred.
/// </para>
/// </remarks>
public record DateTimeValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="DateTime"/> value encapsulated by this Value Object, stored as provided.
    /// </summary>
    public DateTime Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. Using the static <c>Create</c> methods is highly recommended.
    /// </summary>
    /// <param name="value">The DateTime value to encapsulate.</param>
    public DateTimeValueObject(DateTime value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance from a standard <see cref="DateTime"/> value, preserving its kind.
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(DateTime value) => new DateTimeValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by combining the date part of the first <see cref="DateTime"/>
    /// and the time part of the second <see cref="DateTime"/>.
    /// </summary>
    /// <param name="valueDate">The source containing the date part.</param>
    /// <param name="valueTime">The source containing the time part.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(DateTime valueDate, DateTime valueTime) => Create(Convert(valueDate, valueTime));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by combining a <see cref="DateOnly"/> and a <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="valueDate">The source <see cref="DateOnly"/> value.</param>
    /// <param name="valueTime">The source <see cref="TimeOnly"/> value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(DateOnly valueDate, TimeOnly valueTime) => Create(Convert(valueDate, valueTime));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by parsing and combining a date string and a time string.
    /// </summary>
    /// <param name="valueDate">The source string containing the date part.</param>
    /// <param name="valueTime">The source string containing the time part.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if either string cannot be parsed.</exception>
    public static DateTimeValueObject Create(string valueDate, string valueTime) => Create(Convert(valueDate, valueTime));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by parsing a string representation of the date and time.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a DateTime.</exception>
    public static DateTimeValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by converting an integer (interpreted as Ticks).
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(int value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by converting a float (casting to integer first).
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="DateTimeValueObject"/> instance by converting a decimal (casting to integer first).
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>
    /// Returns the <see cref="DateTime"/> value as-is, preserving its <see cref="DateTimeKind"/>.
    /// </summary>
    protected static DateTime Convert(DateTime value) => value;

    /// <summary>
    /// Combines the date part of the first parameter with the time part of the second parameter.
    /// The resulting <see cref="DateTimeKind"/> is not set explicitly.
    /// </summary>
    protected static DateTime Convert(DateTime valueDate, DateTime valueTime) => valueDate.Date.AddHours(valueTime.Hour).AddMinutes(valueTime.Minute).AddSeconds(valueTime.Second).AddMilliseconds(valueTime.Millisecond).AddMicroseconds(valueTime.Microsecond);

    /// <summary>
    /// Combines a <see cref="DateOnly"/> and a <see cref="TimeOnly"/> into a <see cref="DateTime"/> (kind unspecified).
    /// </summary>
    protected static DateTime Convert(DateOnly valueDate, TimeOnly valueTime) => Convert(valueDate.ToDateTime(valueTime));

    /// <summary>
    /// Parses a string into a <see cref="DateTime"/> object, trimming whitespace first.
    /// Note: The resulting <see cref="DateTimeKind"/> depends on the input string format/culture.
    /// </summary>
    protected static DateTime Convert(string value) => Convert(DateTime.Parse(value.Trim()));

    /// <summary>
    /// Converts separate date and time strings by parsing them and combining them using <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
    /// Note: The resulting <see cref="DateTimeKind"/> is not set explicitly.
    /// </summary>
    protected static DateTime Convert(string valueDate, string valueTime) => Convert(DateOnly.FromDateTime(DateTime.Parse(valueDate.Trim())).ToDateTime(TimeOnly.FromDateTime(DateTime.Parse(valueTime.Trim()))));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from an integer interpreted as Ticks (kind unspecified).
    /// </summary>
    protected static DateTime Convert(int value) => Convert(new DateTime(value));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from a float (after casting to integer Ticks, kind unspecified).
    /// </summary>
    protected static DateTime Convert(float value) => Convert(new DateTime((int)value));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from a decimal (after casting to integer Ticks, kind unspecified).
    /// </summary>
    protected static DateTime Convert(decimal value) => Convert(new DateTime((int)value));

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated <see cref="DateTime"/> value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
