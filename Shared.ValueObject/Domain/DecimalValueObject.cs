using FluentValidation;

namespace Shared.ValueObject.Domain;

public record DecimalValueObject : IValueObject
{
    public decimal Value { get; init; }

    public DecimalValueObject(decimal value)
    {
        Value = value;
    }

    public static DecimalValueObject Create(decimal value, IValidator<DecimalValueObject>? validator = null)
    {
        DecimalValueObject ValueObject = new DecimalValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static DecimalValueObject Create(IValidator<DecimalValueObject>? validator = null) => Create(0, validator);
    public static DecimalValueObject Create(string value, IValidator<DecimalValueObject>? validator = null) => Create(Convert(value), validator);
    public static DecimalValueObject Create(int value, IValidator<DecimalValueObject>? validator = null) => Create(Convert(value), validator);
    public static DecimalValueObject Create(float value, IValidator<DecimalValueObject>? validator = null) => Create(Convert(value), validator);
    public static DecimalValueObject Create(bool value, IValidator<DecimalValueObject>? validator = null) => Create(Convert(value), validator);
    public static DecimalValueObject Create(DateTime value, IValidator<DecimalValueObject>? validator = null) => Create(Convert(value), validator);

    protected static decimal Convert(decimal value) => value;
    protected static decimal Convert(string value) => decimal.Parse(value.Trim());
    protected static decimal Convert(int value) => value;
    protected static decimal Convert(float value) => System.Convert.ToDecimal(value);
    protected static decimal Convert(bool value) => value ? 1 : 0;
    protected static decimal Convert(DateTime value) => System.Convert.ToDecimal(new TimeSpan(value.Ticks).TotalSeconds);

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
    public static implicit operator decimal(DecimalValueObject valueObject) => valueObject.Value;
}
