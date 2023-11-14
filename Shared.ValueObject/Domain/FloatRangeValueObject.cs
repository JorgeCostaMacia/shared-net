using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObject : IValueObject
    {
        public FloatValueObject ValueStart { get; init; }
        public FloatValueObject ValueEnd { get; init; }

        public FloatRangeValueObject(FloatValueObject valueStart, FloatValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static FloatRangeValueObject From(FloatValueObject valueStart, FloatValueObject valueEnd, bool validate = true)
        {
            FloatRangeValueObject ValueObject = new FloatRangeValueObject(valueStart, valueEnd);
            if (validate) new FloatRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatRangeValueObject From() => From(0, 0);
        public static FloatRangeValueObject From(float valueStart, float valueEnd, bool validate = true) => From(FloatValueObject.From(valueStart, false), FloatValueObject.From(valueEnd, false), validate);
        public static FloatRangeValueObject From(string valueStart, string valueEnd, bool validate = true) => From(FloatValueObject.From(valueStart, false), FloatValueObject.From(valueEnd, false), validate);
        public static FloatRangeValueObject From(int valueStart, int valueEnd, bool validate = true) => From(FloatValueObject.From(valueStart, false), FloatValueObject.From(valueEnd, false), validate);
        public static FloatRangeValueObject From(decimal valueStart, decimal valueEnd, bool validate = true) => From(FloatValueObject.From(valueStart, false), FloatValueObject.From(valueEnd, false), validate);
        public static FloatRangeValueObject From(DateTime valueStart, DateTime valueEnd, bool validate = true) => From(FloatValueObject.From(valueStart, false), FloatValueObject.From(valueEnd, false), validate);

        public override bool Equals(object? obj) => obj is FloatRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(FloatRangeValueObject? left, FloatRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(FloatRangeValueObject? left, FloatRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
