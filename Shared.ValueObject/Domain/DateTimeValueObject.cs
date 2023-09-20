using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DateTimeValueObject : IValueObject
    {
        public DateTime Value { get; }

        public DateTimeValueObject(DateTime value)
        {
            Value = value;
        }

        public static DateTimeValueObject Create(DateTime value, bool validate = true)
        {
            DateTimeValueObject ValueObject = new DateTimeValueObject(ToValue(value));
            if (validate) new DateTimeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DateTimeValueObject Create() => new DateTimeValueObject(DateTime.UtcNow);
        public static DateTimeValueObject Create(DateTime valueDate, DateTime valueTime, bool validate = true) => Create(ToValue(valueDate, valueTime), validate);
        public static DateTimeValueObject Create(DateOnly valueDate, TimeOnly valueTime, bool validate = true) => Create(ToValue(valueDate, valueTime), validate);
        public static DateTimeValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static DateTimeValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static DateTimeValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);

        protected static DateTime ToValue(DateTime value) => value.ToUniversalTime();
        protected static DateTime ToValue(DateTime valueDate, DateTime valueTime) => valueDate.Date.AddHours(valueDate.Hour).AddMinutes(valueTime.Minute).AddSeconds(valueTime.Second).AddMilliseconds(valueTime.Millisecond).AddMicroseconds(valueTime.Microsecond);
        protected static DateTime ToValue(DateOnly valueDate, TimeOnly valueTime) => valueDate.ToDateTime(valueTime, DateTimeKind.Utc);
        protected static DateTime ToValue(int value) => new DateTime(value, DateTimeKind.Utc);
        protected static DateTime ToValue(float value) => new DateTime((int)value, DateTimeKind.Utc);
        protected static DateTime ToValue(string value) => DateTime.Parse(value);
        protected static DateTime ToValue(string valueDate, string valueTime) => DateOnly.FromDateTime(DateTime.Parse(valueDate)).ToDateTime(TimeOnly.FromDateTime(DateTime.Parse(valueTime)));

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
