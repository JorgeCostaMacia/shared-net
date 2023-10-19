using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class BoolValueObject : IValueObject
    {
        public bool Value { get; init; }

        public BoolValueObject(bool value)
        {
            Value = value;
        }

        public static BoolValueObject From(bool value, bool validate = true)
        {
            BoolValueObject ValueObject = new BoolValueObject(Convert(value));
            if (validate) new BoolValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static BoolValueObject From() => From(true);
        public static BoolValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static BoolValueObject From(int value, bool validate = true) => From(Convert(value), validate);
        public static BoolValueObject From(float value, bool validate = true) => From(Convert(value), validate);

        public static BoolValueObject? FromOrDefault(bool? value, bool validate = true) => value != null ? From((bool)value, validate) : From();
        public static BoolValueObject? FromOrDefault(string? value, bool validate = true) => value != null ? From(value, validate) : From();
        public static BoolValueObject? FromOrDefault(int? value, bool validate = true) => value != null ? From((int)value, validate) : From();
        public static BoolValueObject? FromOrDefault(float? value, bool validate = true) => value != null ? From((float)value, validate) : From();

        protected static bool Convert(bool value) => value;
        protected static bool Convert(string value) => value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "1" || value.Trim().ToUpper() == "SI" || value.Trim().ToUpper() == "YES";
        protected static bool Convert(int value) => value == 1;
        protected static bool Convert(float value) => (int)value == 1;

        public override bool Equals(object? obj) => obj is BoolValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(BoolValueObject? left, BoolValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(BoolValueObject left, BoolValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
