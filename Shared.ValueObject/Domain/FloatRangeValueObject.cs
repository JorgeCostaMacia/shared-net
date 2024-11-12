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

    public static FloatRangeValueObject Create(FloatValueObject valueStart, FloatValueObject valueEnd, IValidator<FloatRangeValueObject>? validator = null)
    {
        FloatRangeValueObject ValueObject = new FloatRangeValueObject(valueStart, valueEnd);
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static FloatRangeValueObject Create(IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(0), FloatValueObject.Create(0), validator);
    public static FloatRangeValueObject Create(float valueStart, float valueEnd, IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd), validator);
    public static FloatRangeValueObject Create(string valueStart, string valueEnd, IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd), validator);
    public static FloatRangeValueObject Create(int valueStart, int valueEnd, IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd), validator);
    public static FloatRangeValueObject Create(decimal valueStart, decimal valueEnd, IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd), validator);
    public static FloatRangeValueObject Create(DateTime valueStart, DateTime valueEnd, IValidator<FloatRangeValueObject>? validator = null) => Create(FloatValueObject.Create(valueStart), FloatValueObject.Create(valueEnd), validator);

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
