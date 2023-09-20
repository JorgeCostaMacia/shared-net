using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class StringValueObject : IValueObject
    {
        public string Value { get; }

        public StringValueObject(string value)
        {
            Value = value;
        }

        public static StringValueObject Create(string value, bool validate = true)
        {
            StringValueObject ValueObject = new StringValueObject(ToValue(value));
            if (validate) new StringValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static StringValueObject Create() => new StringValueObject(string.Empty);
        public static StringValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static StringValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static StringValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static StringValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
        public static StringValueObject Create(Guid value, bool validate = true) => Create(ToValue(value), validate);

        protected static string ToValue(string value) => value.Trim();
        protected static string ToValue(int value) => value.ToString();
        protected static string ToValue(float value) => value.ToString();
        protected static string ToValue(bool value) => value.ToString();
        protected static string ToValue(DateTime value) => value.ToString();
        protected static string ToValue(Guid value) => value.ToString();

        public override bool Equals(object? obj) => obj is StringValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(StringValueObject? left, StringValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(StringValueObject left, StringValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
