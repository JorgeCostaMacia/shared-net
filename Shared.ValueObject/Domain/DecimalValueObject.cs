using FluentValidation;

namespace Shared.ValueObject.Domain;

public record DecimalValueObject : IValueObject
{
    public decimal Value { get; init; }

    public DecimalValueObject(decimal value)
    {
        Value = value;
    }

    public DecimalValueObject Validate(IValidator<DecimalValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static DecimalValueObject Create(decimal value) => new DecimalValueObject(Convert(value));
    public static DecimalValueObject Create() => Create(0);
    public static DecimalValueObject Create(string value) => Create(Convert(value));
    public static DecimalValueObject Create(int value) => Create(Convert(value));
    public static DecimalValueObject Create(float value) => Create(Convert(value));
    public static DecimalValueObject Create(bool value) => Create(Convert(value));
    public static DecimalValueObject Create(DateTime value) => Create(Convert(value));

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
