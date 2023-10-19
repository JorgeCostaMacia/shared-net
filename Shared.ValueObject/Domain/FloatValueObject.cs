using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObject : IValueObject
    {
        public float Value { get; init; }

        public FloatValueObject(float value)
        {
            Value = value;
        }

        public static FloatValueObject From(float value, bool validate = true)
        {
            FloatValueObject ValueObject = new FloatValueObject(Convert(value));
            if (validate) new FloatValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatValueObject From() => From(0);
        public static FloatValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static FloatValueObject From(int value, bool validate = true) => From(Convert(value), validate);
        public static FloatValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);
        public static FloatValueObject From(bool value, bool validate = true) => From(Convert(value), validate);
        public static FloatValueObject From(DateTime value, bool validate = true) => From(Convert(value), validate);

        public static FloatValueObject? FromOrDefault(float? value, bool validate = true) => value != null ? From((float)value, validate) : From();
        public static FloatValueObject? FromOrDefault(string? value, bool validate = true) => value != null ? From(value, validate) : From();
        public static FloatValueObject? FromOrDefault(int? value, bool validate = true) => value != null ? From((int)value, validate) : From();
        public static FloatValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null ? From((decimal)value, validate) : From();
        public static FloatValueObject? FromOrDefault(bool? value, bool validate = true) => value != null ? From((bool)value, validate) : From();
        public static FloatValueObject? FromOrDefault(DateTime? value, bool validate = true) => value != null ? From((DateTime)value, validate) : From();

        protected static float Convert(float value) => value;
        protected static float Convert(string value) => float.Parse(value.Trim());
        protected static float Convert(int value) => value;
        protected static float Convert(decimal value) => (float)value;
        protected static float Convert(bool value) => value ? 1 : 0;
        protected static float Convert(DateTime value) => (float)new TimeSpan(value.Ticks).TotalSeconds;

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
