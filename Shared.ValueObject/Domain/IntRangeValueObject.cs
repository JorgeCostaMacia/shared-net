using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObject : RangeValueObject<int>
    {
        public IntRangeValueObject(int valueStart, int valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static new IntRangeValueObject Create(int valueStart, int valueEnd, bool validate = true)
        {
            IntRangeValueObject ValueObject = new IntRangeValueObject(ToValue(valueStart), ToValue(valueEnd));
            if (validate) new IntRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static IntRangeValueObject Create() => new IntRangeValueObject(0, 0);
        public static IntRangeValueObject Create(IntValueObject valueStart, IntValueObject valueEnd, bool validate = true) => Create(valueStart.Value, valueEnd.Value, validate);

        protected static int ToValue(int value) => value;
    }
}
