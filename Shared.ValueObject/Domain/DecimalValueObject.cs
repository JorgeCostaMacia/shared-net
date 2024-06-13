using FluentValidation;

namespace Shared.ValueObject.Domain;

public class DecimalValueObject(decimal value) : IValueObject
{
    public decimal Value { get; init; } = value;

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

    public override bool Equals(object? obj) => obj is DecimalValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();

    public static bool operator ==(DecimalValueObject? left, DecimalValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
    public static bool operator !=(DecimalValueObject? left, DecimalValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    public static bool operator >(DecimalValueObject left, DecimalValueObject right) => left.Value > right.Value;
    public static bool operator <(DecimalValueObject left, DecimalValueObject right) => left.Value < right.Value;
    public static bool operator >=(DecimalValueObject left, DecimalValueObject right) => left.Value >= right.Value;
    public static bool operator <=(DecimalValueObject left, DecimalValueObject right) => left.Value <= right.Value;
}
