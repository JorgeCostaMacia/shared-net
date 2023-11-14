using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObject : IValueObject
    {
        public DateTimeValueObject ValueStart { get; init; }
        public DateTimeValueObject ValueEnd { get; init; }

        public DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static DateTimeRangeValueObject From(DateTimeValueObject valueStart, DateTimeValueObject valueEnd, bool validate = true)
        {
            DateTimeRangeValueObject ValueObject = new DateTimeRangeValueObject(valueStart, valueEnd);
            if (validate) new DateTimeRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DateTimeRangeValueObject From() => From(DateTime.UtcNow.Date, DateTime.UtcNow.Date);
        public static DateTimeRangeValueObject From(DateTime valueStart, DateTime valueEnd, bool validate = true) => From(DateTimeValueObject.From(valueStart, false), DateTimeValueObject.From(valueEnd, false), validate);
        public static DateTimeRangeValueObject From(string valueStart, string valueEnd, bool validate = true) => From(DateTimeValueObject.From(valueStart, false), DateTimeValueObject.From(valueEnd, false), validate);
        public static DateTimeRangeValueObject From(int valueStart, int valueEnd, bool validate = true) => From(DateTimeValueObject.From(valueStart, false), DateTimeValueObject.From(valueEnd, false), validate);
        public static DateTimeRangeValueObject From(float valueStart, float valueEnd, bool validate = true) => From(DateTimeValueObject.From(valueStart, false), DateTimeValueObject.From(valueEnd, false), validate);
        public static DateTimeRangeValueObject From(decimal valueStart, decimal valueEnd, bool validate = true) => From(DateTimeValueObject.From(valueStart, false), DateTimeValueObject.From(valueEnd, false), validate);

        public override bool Equals(object? obj) => obj is DateTimeRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
