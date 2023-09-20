using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObject : IValueObject
    {
        public float Value { get; }

        public FloatValueObject(float value)
        {
            Value = value;
        }

        public static FloatValueObject Create(float value, bool validate = true)
        {
            FloatValueObject ValueObject = new FloatValueObject(ToValue(value));
            if (validate) new FloatValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatValueObject Create() => new FloatValueObject(0);
        public static FloatValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static FloatValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);
        public static FloatValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static FloatValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);

        protected static float ToValue(float value) => value;
        protected static float ToValue(string value) => float.Parse(value);
        protected static float ToValue(int value) => value;
        protected static float ToValue(bool value) => value ? 1 : 0;
        protected static float ToValue(DateTime value) => (float)new TimeSpan(value.Ticks).TotalSeconds;

        public override bool Equals(object? obj) => obj is FloatValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(FloatValueObject? left, FloatValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(FloatValueObject? left, FloatValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
        public static bool operator >(FloatValueObject left, FloatValueObject right) => left.Value > right.Value;
        public static bool operator <(FloatValueObject left, FloatValueObject right) => left.Value < right.Value;
        public static bool operator >=(FloatValueObject left, FloatValueObject right) => left.Value >= right.Value;
        public static bool operator <=(FloatValueObject left, FloatValueObject right) => left.Value <= right.Value;
    }
}
