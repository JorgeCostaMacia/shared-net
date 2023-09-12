using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DecimalValueObject : IValueObject
    {
        public decimal Value { get; }

        public DecimalValueObject(decimal value)
        {
            Value = value;
        }

        public static DecimalValueObject Create(decimal value, bool validate = true)
        {
            DecimalValueObject ValueObject = new DecimalValueObject(ToValue(value));
            if (validate) new DecimalValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DecimalValueObject Create() => new DecimalValueObject(0);
        public static DecimalValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static DecimalValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);
        public static DecimalValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static DecimalValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);

        protected static decimal ToValue(decimal value) => value;
        protected static decimal ToValue(string value) => decimal.Parse(value);
        protected static decimal ToValue(int value) => value;
        protected static decimal ToValue(bool value) => value ? 1 : 0;
        protected static decimal ToValue(DateTime value) => (decimal)new TimeSpan(value.Ticks).TotalSeconds;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static bool operator >(DecimalValueObject left, DecimalValueObject right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(DecimalValueObject left, DecimalValueObject right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >=(DecimalValueObject left, DecimalValueObject right)
        {
            return left.Value >= right.Value;
        }

        public static bool operator <=(DecimalValueObject left, DecimalValueObject right)
        {
            return left.Value <= right.Value;
        }
    }
}
