using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IntValueObject : IValueObject
    {
        public int Value { get; init; }

        public IntValueObject(int value)
        {
            Value = value;
        }

        public static IntValueObject From(int value, bool validate = true)
        {
            IntValueObject ValueObject = new IntValueObject(Convert(value));
            if (validate) new IntValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static IntValueObject From() => From(0);
        public static IntValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static IntValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static IntValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);
        public static IntValueObject From(bool value, bool validate = true) => From(Convert(value), validate);
        public static IntValueObject From(DateTime value, bool validate = true) => From(Convert(value), validate);

        public static IntValueObject? FromOrDefault(int? value, bool validate = true) => value != null ? From((int)value, validate) : From();
        public static IntValueObject? FromOrDefault(string? value, bool validate = true) => value != null ? From(value, validate) : From();
        public static IntValueObject? FromOrDefault(float? value, bool validate = true) => value != null ? From((float)value, validate) : From();
        public static IntValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null ? From((decimal)value, validate) : From();
        public static IntValueObject? FromOrDefault(bool? value, bool validate = true) => value != null ? From((bool)value, validate) : From();
        public static IntValueObject? FromOrDefault(DateTime? value, bool validate = true) => value != null ? From((DateTime)value, validate) : From();

        protected static int Convert(int value) => value;
        protected static int Convert(string value) => System.Convert.ToInt32(value.Trim());
        protected static int Convert(float value) => System.Convert.ToInt32(value);
        protected static int Convert(decimal value) => System.Convert.ToInt32(value);
        protected static int Convert(bool value) => System.Convert.ToInt32(value);
        protected static int Convert(DateTime value) => System.Convert.ToInt32(value);

        public override bool Equals(object? obj) => obj is IntValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(IntValueObject? left, IntValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(IntValueObject? left, IntValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
        public static bool operator >(IntValueObject left, IntValueObject right) => left.Value > right.Value;
        public static bool operator <(IntValueObject left, IntValueObject right) => left.Value < right.Value;
        public static bool operator >=(IntValueObject left, IntValueObject right) => left.Value >= right.Value;
        public static bool operator <=(IntValueObject left, IntValueObject right) => left.Value <= right.Value;
    }
}
