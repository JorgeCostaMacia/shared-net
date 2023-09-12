using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObject : IntRangeValueObject
    {
        public PageNumberRangeValueObject(int valueStart, int valueEnd) : base(valueStart, valueEnd)
        {
        }
        public static new PageNumberRangeValueObject Create(int valueStart, int valueEnd, bool validate = true)
        {
            PageNumberRangeValueObject ValueObject = new PageNumberRangeValueObject(ToValue(valueStart), ToValue(valueEnd));
            if (validate) new PageNumberRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberRangeValueObject Create() => new PageNumberRangeValueObject(1, 1);
        public static PageNumberRangeValueObject Create(PageNumberValueObject valueStart, PageNumberValueObject valueEnd, bool validate = true) => Create(valueStart.Value, valueEnd.Value, validate);

        protected static new int ToValue(int value) => value;
    }
}
