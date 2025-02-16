using FluentValidation;

namespace Shared.ValueObject.Domain;

public record FloatValueObject : IValueObject
{
    public float Value { get; init; }

    public FloatValueObject(float value)
    {
        Value = value;
    }

    public FloatValueObject Validate(IValidator<FloatValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static FloatValueObject Create(float value) => new FloatValueObject(Convert(value));
    public static FloatValueObject Create() => Create(0);
    public static FloatValueObject Create(string value) => Create(Convert(value));
    public static FloatValueObject Create(int value) => Create(Convert(value));
    public static FloatValueObject Create(decimal value) => Create(Convert(value));
    public static FloatValueObject Create(bool value) => Create(Convert(value));
    public static FloatValueObject Create(DateTime value) => Create(Convert(value));

    protected static float Convert(float value) => value;
    protected static float Convert(string value) => float.Parse(value.Trim());
    protected static float Convert(int value) => value;
    protected static float Convert(decimal value) => (float)value;
    protected static float Convert(bool value) => value ? 1 : 0;
    protected static float Convert(DateTime value) => (float)new TimeSpan(value.Ticks).TotalSeconds;

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
    public static implicit operator float(FloatValueObject valueObject) => valueObject.Value;
}
