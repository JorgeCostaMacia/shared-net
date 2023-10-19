using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DateTimeValueObject : IValueObject
    {
        public DateTime Value { get; init; }

        public DateTimeValueObject(DateTime value)
        {
            Value = value;
        }

        public static DateTimeValueObject From(DateTime value, bool validate = true)
        {
            DateTimeValueObject ValueObject = new DateTimeValueObject(Convert(value));
            if (validate) new DateTimeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DateTimeValueObject From() => From(DateTime.UtcNow);
        public static DateTimeValueObject From(DateTime valueDate, DateTime valueTime, bool validate = true) => From(Convert(valueDate, valueTime), validate);
        public static DateTimeValueObject From(DateOnly valueDate, TimeOnly valueTime, bool validate = true) => From(Convert(valueDate, valueTime), validate);
        public static DateTimeValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static DateTimeValueObject From(int value, bool validate = true) => From(Convert(value), validate);
        public static DateTimeValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static DateTimeValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);

        public static DateTimeValueObject? FromOrDefault(DateTime? value, bool validate = true) => value != null ? From((DateTime)value, validate) : From();
        public static DateTimeValueObject? FromOrDefault(DateTime? valueDate, DateTime? valueTime, bool validate = true) => valueDate != null && valueTime != null ? From((DateTime)valueDate, (DateTime)valueTime, validate) : From();
        public static DateTimeValueObject? FromOrDefault(DateOnly? valueDate, TimeOnly? valueTime, bool validate = true) => valueDate != null && valueTime != null ? From((DateOnly)valueDate, (TimeOnly)valueTime, validate) : From();
        public static DateTimeValueObject? FromOrDefault(string? value, bool validate = true) => value != null && value.Trim() != "" ? From(value, validate) : From();
        public static DateTimeValueObject? FromOrDefault(int? value, bool validate = true) => value != null && value != 0 ? From((int)value, validate) : From();
        public static DateTimeValueObject? FromOrDefault(float? value, bool validate = true) => value != null && value != 0 ? From((float)value, validate) : From();
        public static DateTimeValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null && value != 0 ? From((decimal)value, validate) : From();

        protected static DateTime Convert(DateTime value) => value.ToUniversalTime();
        protected static DateTime Convert(DateTime valueDate, DateTime valueTime) => valueDate.Date.AddHours(valueDate.Hour).AddMinutes(valueTime.Minute).AddSeconds(valueTime.Second).AddMilliseconds(valueTime.Millisecond).AddMicroseconds(valueTime.Microsecond);
        protected static DateTime Convert(DateOnly valueDate, TimeOnly valueTime) => valueDate.ToDateTime(valueTime, DateTimeKind.Utc);
        protected static DateTime Convert(string value) => DateTime.Parse(value.Trim());
        protected static DateTime Convert(string valueDate, string valueTime) => DateOnly.FromDateTime(DateTime.Parse(valueDate.Trim())).ToDateTime(TimeOnly.FromDateTime(DateTime.Parse(valueTime.Trim())));
        protected static DateTime Convert(int value) => new DateTime(value, DateTimeKind.Utc);
        protected static DateTime Convert(float value) => new DateTime((int)value, DateTimeKind.Utc);
        protected static DateTime Convert(decimal value) => new DateTime((int)value, DateTimeKind.Utc);

        public override bool Equals(object? obj) => obj is DateTimeValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(DateTimeValueObject? left, DateTimeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(DateTimeValueObject? left, DateTimeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
        public static bool operator >(DateTimeValueObject left, DateTimeValueObject right) => left.Value > right.Value;
        public static bool operator <(DateTimeValueObject left, DateTimeValueObject right) => left.Value < right.Value;
        public static bool operator >=(DateTimeValueObject left, DateTimeValueObject right) => left.Value >= right.Value;
        public static bool operator <=(DateTimeValueObject left, DateTimeValueObject right) => left.Value <= right.Value;
    }
}
