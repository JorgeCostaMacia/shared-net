namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="bool"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on boolean logic (e.g., IsActive, IsPublished).
/// It guarantees immutability and provides robust static factory methods for conversion from various primitive types.
/// </para>
/// <para>
/// The public constructor is primarily intended for direct instantiation in infrastructure layers (e.g., ORM mapping, deserialization).
/// For domain logic and safety, the static <c>Create</c> factory methods are highly recommended.
/// </para>
/// </remarks>
public record BoolValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="bool"/> value encapsulated by this Value Object.
    /// </summary>
    public bool Value { get; init; }

    /// <summary>
    /// <b>Primary Constructor.</b> Initializes the Value Object.
    /// This constructor bypasses validation logic. While public, using the static <c>Create</c> methods is highly recommended
    /// to ensure type conversion and adherence to best practices.
    /// </summary>
    /// <param name="value">The boolean value to encapsulate.</param>
    public BoolValueObject(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance from a boolean value.
    /// </summary>
    /// <param name="value">The source boolean value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(bool value) => new BoolValueObject(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting a string value to a boolean.
    /// Supports conversion of "TRUE", "1", "SI", or "YES" (case-insensitive) to <c>true</c>.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(string value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting an integer value to a boolean.
    /// Converts only <c>1</c> to <c>true</c>.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(int value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting a float value to a boolean.
    /// Converts only <c>1.0</c> to <c>true</c>.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(float value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting a long value to a boolean.
    /// Converts only <c>1</c> to <c>true</c>.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(long value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting a double value to a boolean.
    /// Converts only <c>1</c> to <c>true</c>.
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(double value) => Create(Convert(value));

    /// <summary>
    /// Creates a new <see cref="BoolValueObject"/> instance by converting a decimal value to a boolean.
    /// Converts only <c>1</c> to <c>true</c>.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new <see cref="BoolValueObject"/> instance.</returns>
    public static BoolValueObject Create(decimal value) => Create(Convert(value));

    /// <summary>
    /// Converts a boolean value (identity conversion).
    /// </summary>
    protected static bool Convert(bool value) => value;

    /// <summary>
    /// Converts a string to a boolean. Supports "TRUE", "1", "SI", or "YES" (case-insensitive).
    /// </summary>
    protected static bool Convert(string value) => Convert(value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "1" || value.Trim().ToUpper() == "SI" || value.Trim().ToUpper() == "YES");

    /// <summary>
    /// Converts an integer to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(int value) => Convert(value == 1);

    /// <summary>
    /// Converts a float to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(float value) => Convert((int)value == 1);

    /// <summary>
    /// Converts a long to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(long value) => Convert(value == 1);

    /// <summary>
    /// Converts a double to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(double value) => Convert((int)value == 1);

    /// <summary>
    /// Converts a decimal to a boolean. Only <c>1</c> maps to <c>true</c>.
    /// </summary>
    protected static bool Convert(decimal value) => Convert((int)value == 1);

    /// <summary>
    /// Generates the hash code based on the internal value (<see cref="Value"/>).
    /// Overrides the base method to ensure correct Value Object comparison.
    /// </summary>
    /// <returns>The object's hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Returns the string representation of the encapsulated boolean value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string ("True" or "False").</returns>
    public override string ToString() => Value.ToString();
}