using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class StringValueObject : IValueObject
    {
        public string Value { get; init; }

        public StringValueObject(string value)
        {
            Value = value;
        }

        public static StringValueObject From(string value, bool validate = true)
        {
            StringValueObject ValueObject = new StringValueObject(Convert(value));
            if (validate) new StringValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static StringValueObject From() => From(string.Empty);
        public static StringValueObject From(int value, bool validate = true) => From(Convert(value), validate);
        public static StringValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static StringValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);
        public static StringValueObject From(bool value, bool validate = true) => From(Convert(value), validate);
        public static StringValueObject From(DateTime value, bool validate = true) => From(Convert(value), validate);
        public static StringValueObject From(Guid value, bool validate = true) => From(Convert(value), validate);

        public static StringValueObject? FromOrDefault(string? value, bool validate = true) => value != null ? From(value, validate) : From();
        public static StringValueObject? FromOrDefault(int? value, bool validate = true) => value != null ? From((int)value, validate) : From();
        public static StringValueObject? FromOrDefault(float? value, bool validate = true) => value != null ? From((float)value, validate) : From();
        public static StringValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null ? From((decimal)value, validate) : From();
        public static StringValueObject? FromOrDefault(bool? value, bool validate = true) => value != null ? From((bool)value, validate) : From();
        public static StringValueObject? FromOrDefault(DateTime? value, bool validate = true) => value != null ? From((DateTime)value, validate) : From();
        public static StringValueObject? FromOrDefault(Guid? value, bool validate = true) => value != null ? From((Guid)value, validate) : From();

        protected static string Convert(string value) => value.Trim();
        protected static string Convert(int value) => value.ToString();
        protected static string Convert(float value) => value.ToString();
        protected static string Convert(decimal value) => value.ToString();
        protected static string Convert(bool value) => value.ToString();
        protected static string Convert(DateTime value) => value.ToString();
        protected static string Convert(Guid value) => value.ToString();

        public override bool Equals(object? obj) => obj is StringValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(StringValueObject? left, StringValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(StringValueObject left, StringValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
