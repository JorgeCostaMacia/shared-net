using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IntValueObject : IValueObject
    {
        public int Value { get; }

        public IntValueObject(int value)
        {
            Value = value;
        }

        public static IntValueObject Create(int value, bool validate = true)
        {
            IntValueObject ValueObject = new IntValueObject(ToValue(value));
            if (validate) new IntValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static IntValueObject Create() => new IntValueObject(0);
        public static IntValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static IntValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);
        public static IntValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static IntValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);

        protected static int ToValue(int value) => value;
        protected static int ToValue(string value) => Convert.ToInt32(value);
        protected static int ToValue(float value) => Convert.ToInt32(value);
        protected static int ToValue(bool value) => Convert.ToInt32(value);
        protected static int ToValue(DateTime value) => Convert.ToInt32(value);

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
