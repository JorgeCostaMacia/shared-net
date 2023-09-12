using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class RangeValueObject<T> : IValueObject
    {
        public T ValueStart { get; }
        public T ValueEnd { get; }

        public RangeValueObject(T valueStart, T valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static RangeValueObject<T> Create(T valueStart, T valueEnd, bool validate = true)
        {
            RangeValueObject<T> ValueObject = new RangeValueObject<T>(valueStart, valueEnd);
            if (validate) new RangeValueObjectValidator<T>().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ValueStart;
            yield return ValueEnd;
        }
    }
}
