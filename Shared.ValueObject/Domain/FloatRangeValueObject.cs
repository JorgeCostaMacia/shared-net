using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObject : IValueObject
    {
        public FloatValueObject ValueStart { get; }
        public FloatValueObject ValueEnd { get; }

        public FloatRangeValueObject(FloatValueObject valueStart, FloatValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static FloatRangeValueObject Create(FloatValueObject valueStart, FloatValueObject valueEnd, bool validate = true)
        {
            FloatRangeValueObject ValueObject = new FloatRangeValueObject(valueStart, valueEnd);
            if (validate) new FloatRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatRangeValueObject Create() => new FloatRangeValueObject(FloatValueObject.Create(0), FloatValueObject.Create(0));
        public static FloatRangeValueObject Create(float valueStart, float valueEnd, bool validate = true) => Create(FloatValueObject.Create(valueStart, false), FloatValueObject.Create(valueEnd, false), validate);

        public override bool Equals(object? obj) => obj is FloatRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(FloatRangeValueObject? left, FloatRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(FloatRangeValueObject? left, FloatRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
