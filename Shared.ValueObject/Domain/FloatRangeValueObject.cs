using FluentValidation;

namespace Shared.ValueObject.Domain;

public record FloatRangeValueObject : IValueObject
{
    public FloatValueObject ValueStart { get; init; }
    public FloatValueObject ValueEnd { get; init; }

    public FloatRangeValueObject(FloatValueObject valueStart, FloatValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    public FloatRangeValueObject Validate(IValidator<FloatRangeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static FloatRangeValueObject Create(FloatValueObject valueStart, FloatValueObject valueEnd) => new FloatRangeValueObject(valueStart, valueEnd);
    public static FloatRangeValueObject Create() => Create(FloatValueObject.Create(0), FloatValueObject.Create(0));
    public static FloatRangeValueObject Create(float valueStart, float valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));
    public static FloatRangeValueObject Create(string valueStart, string valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));
    public static FloatRangeValueObject Create(int valueStart, int valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));
    public static FloatRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));
    public static FloatRangeValueObject Create(DateTime valueStart, DateTime valueEnd) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd));

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
