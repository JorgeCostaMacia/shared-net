using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObject : IValueObject
    {
        public DateTimeValueObject ValueStart { get; }
        public DateTimeValueObject ValueEnd { get; }

        public DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static DateTimeRangeValueObject Create(DateTimeValueObject valueStart, DateTimeValueObject valueEnd, bool validate = true)
        {
            DateTimeRangeValueObject ValueObject = new DateTimeRangeValueObject(valueStart, valueEnd);
            if (validate) new DateTimeRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DateTimeRangeValueObject Create() => new DateTimeRangeValueObject(DateTimeValueObject.Create(DateTime.UtcNow.Date), DateTimeValueObject.Create(DateTime.UtcNow.Date));
        public static DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd, bool validate = true) => Create(DateTimeValueObject.Create(valueStart, false), DateTimeValueObject.Create(valueEnd, false), validate);

        public override bool Equals(object? obj) => obj is DateTimeRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
