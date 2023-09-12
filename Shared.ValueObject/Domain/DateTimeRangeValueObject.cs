using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObject : RangeValueObject<DateTime>
    {
        public DateTimeRangeValueObject(DateTime valueStart, DateTime valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static new DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd, bool validate = true)
        {
            DateTimeRangeValueObject ValueObject = new DateTimeRangeValueObject(ToValue(valueStart), ToValue(valueEnd));
            if (validate) new DateTimeRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static DateTimeRangeValueObject Create() => new DateTimeRangeValueObject(DateTime.UtcNow.Date, DateTime.UtcNow.Date);
        public static DateTimeRangeValueObject Create(DateTimeValueObject valueStart, DateTimeValueObject valueEnd, bool validate = true) => Create(valueStart.Value, valueEnd.Value, validate);

        protected static DateTime ToValue(DateTime value) => value.ToUniversalTime();
    }
}
