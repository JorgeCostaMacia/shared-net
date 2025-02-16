using FluentValidation;

namespace Shared.ValueObject.Domain;

public record BoolValueObject : IValueObject
{
    public bool Value { get; init; }

    public BoolValueObject(bool value)
    {
        Value = value;
    }

    public BoolValueObject Validate(IValidator<BoolValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static BoolValueObject Create(bool value) => new BoolValueObject(Convert(value));
    public static BoolValueObject Create() => Create(true);
    public static BoolValueObject Create(string value) => Create(Convert(value));
    public static BoolValueObject Create(int value) => Create(Convert(value));
    public static BoolValueObject Create(float value) => Create(Convert(value));

    protected static bool Convert(bool value) => value;
    protected static bool Convert(string value) => value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "1" || value.Trim().ToUpper() == "SI" || value.Trim().ToUpper() == "YES";
    protected static bool Convert(int value) => value == 1;
    protected static bool Convert(float value) => (int)value == 1;

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();

    public static implicit operator bool(BoolValueObject valueObject) => valueObject.Value;
}