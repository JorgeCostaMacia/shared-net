using FluentValidation;

namespace Shared.ValueObject.Domain;

public record IntValueObject : IValueObject
{
    public int Value { get; init; }

    public IntValueObject(int value)
    {
        Value = value;
    }

    public IntValueObject Validate(IValidator<IntValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static IntValueObject Create(int value) => new IntValueObject(Convert(value));
    public static IntValueObject Create() => Create(0);
    public static IntValueObject Create(string value) => Create(Convert(value));
    public static IntValueObject Create(float value) => Create(Convert(value));
    public static IntValueObject Create(decimal value) => Create(Convert(value));
    public static IntValueObject Create(bool value) => Create(Convert(value));
    public static IntValueObject Create(DateTime value) => Create(Convert(value));

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
