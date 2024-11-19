using FluentValidation;

namespace Shared.ValueObject.Domain;

public record IntValueObject : IValueObject
{
    public int Value { get; init; }

    public IntValueObject(int value)
    {
        Value = value;
    }

    public static IntValueObject Create(int value, IValidator<IntValueObject>? validator = null)
    {
        IntValueObject ValueObject = new IntValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static IntValueObject Create(IValidator<IntValueObject>? validator = null) => Create(0, validator);
    public static IntValueObject Create(string value, IValidator<IntValueObject>? validator = null) => Create(Convert(value), validator);
    public static IntValueObject Create(float value, IValidator<IntValueObject>? validator = null) => Create(Convert(value), validator);
    public static IntValueObject Create(decimal value, IValidator<IntValueObject>? validator = null) => Create(Convert(value), validator);
    public static IntValueObject Create(bool value, IValidator<IntValueObject>? validator = null) => Create(Convert(value), validator);
    public static IntValueObject Create(DateTime value, IValidator<IntValueObject>? validator = null) => Create(Convert(value), validator);

    protected static int Convert(int value) => value;
    protected static int Convert(string value) => System.Convert.ToInt32(float.Parse(value.Trim()));
    protected static int Convert(float value) => System.Convert.ToInt32(value);
    protected static int Convert(decimal value) => System.Convert.ToInt32(value);
    protected static int Convert(bool value) => System.Convert.ToInt32(value);
    protected static int Convert(DateTime value) => System.Convert.ToInt32(value);

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
    public static implicit operator int(IntValueObject valueObject) => valueObject.Value;
}
