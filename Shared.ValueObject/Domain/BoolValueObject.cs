using FluentValidation;

namespace Shared.ValueObject.Domain;

public class BoolValueObject : IValueObject
{
    public bool Value { get; init; }

    public BoolValueObject(bool value)
    {
        Value = value;
    }

    public static BoolValueObject Create(bool value, IValidator<BoolValueObject>? validator = null)
    {
        BoolValueObject ValueObject = new BoolValueObject(Convert(value));
        validator?.ValidateAndThrowAsync(ValueObject);

        return ValueObject;
    }

    public static BoolValueObject Create(IValidator<BoolValueObject>? validator = null) => Create(true, validator);
    public static BoolValueObject Create(string value, IValidator<BoolValueObject>? validator = null) => Create(Convert(value), validator);
    public static BoolValueObject Create(int value, IValidator<BoolValueObject>? validator = null) => Create(Convert(value), validator);
    public static BoolValueObject Create(float value, IValidator<BoolValueObject>? validator = null) => Create(Convert(value), validator);

    protected static bool Convert(bool value) => value;
    protected static bool Convert(string value) => value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "1" || value.Trim().ToUpper() == "SI" || value.Trim().ToUpper() == "YES";
    protected static bool Convert(int value) => value == 1;
    protected static bool Convert(float value) => (int)value == 1;

    public override bool Equals(object? obj) => obj is BoolValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();

    public static bool operator ==(BoolValueObject? left, BoolValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
    public static bool operator !=(BoolValueObject left, BoolValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
}
