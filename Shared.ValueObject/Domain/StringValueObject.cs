using FluentValidation;

namespace Shared.ValueObject.Domain;

public record StringValueObject : IValueObject
{
    public string Value { get; init; }

    public StringValueObject(string value)
    {
        Value = value;
    }

    public static StringValueObject Create(string value, IValidator<StringValueObject>? validator = null)
    {
        StringValueObject ValueObject = new StringValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static StringValueObject Create(IValidator<StringValueObject>? validator = null) => Create(string.Empty, validator);
    public static StringValueObject Create(int value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);
    public static StringValueObject Create(float value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);
    public static StringValueObject Create(decimal value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);
    public static StringValueObject Create(bool value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);
    public static StringValueObject Create(DateTime value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);
    public static StringValueObject Create(Guid value, IValidator<StringValueObject>? validator = null) => Create(Convert(value), validator);

    protected static string Convert(string value) => value.Trim();
    protected static string Convert(int value) => value.ToString();
    protected static string Convert(float value) => value.ToString();
    protected static string Convert(decimal value) => value.ToString();
    protected static string Convert(bool value) => value.ToString();
    protected static string Convert(DateTime value) => value.ToString();
    protected static string Convert(Guid value) => value.ToString();

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
}