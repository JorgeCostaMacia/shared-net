using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DecimalValueObject : IValueObject
    {
        public decimal Value { get; init; }

        public DecimalValueObject(decimal value)
        {
            Value = value;
        }

        public static DecimalValueObject From(decimal value, bool validate = true)
        {
            DecimalValueObject ValueObject = new DecimalValueObject(Convert(value));
            if (validate) new DecimalValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DecimalValueObject From() => From(0);
        public static DecimalValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static DecimalValueObject From(int value, bool validate = true) => From(Convert(value), validate);
        public static DecimalValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static DecimalValueObject From(bool value, bool validate = true) => From(Convert(value), validate);
        public static DecimalValueObject From(DateTime value, bool validate = true) => From(Convert(value), validate);

        protected static decimal Convert(decimal value) => value;
        protected static decimal Convert(string value) => decimal.Parse(value.Trim());
        protected static decimal Convert(int value) => value;
        protected static decimal Convert(float value) => System.Convert.ToDecimal(value);
        protected static decimal Convert(bool value) => value ? 1 : 0;
        protected static decimal Convert(DateTime value) => System.Convert.ToDecimal(new TimeSpan(value.Ticks).TotalSeconds);

        public override bool Equals(object? obj) => obj is DecimalValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(DecimalValueObject? left, DecimalValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(DecimalValueObject? left, DecimalValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
        public static bool operator >(DecimalValueObject left, DecimalValueObject right) => left.Value > right.Value;
        public static bool operator <(DecimalValueObject left, DecimalValueObject right) => left.Value < right.Value;
        public static bool operator >=(DecimalValueObject left, DecimalValueObject right) => left.Value >= right.Value;
        public static bool operator <=(DecimalValueObject left, DecimalValueObject right) => left.Value <= right.Value;
    }
}
