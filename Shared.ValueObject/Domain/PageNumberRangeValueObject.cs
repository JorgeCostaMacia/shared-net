using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObject : IntRangeValueObject
    {
        public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static PageNumberRangeValueObject Create(PageNumberValueObject valueStart, PageNumberValueObject valueEnd, bool validate = true)
        {
            PageNumberRangeValueObject ValueObject = new PageNumberRangeValueObject(valueStart, valueEnd);
            if (validate) new PageNumberRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberRangeValueObject Create() => new PageNumberRangeValueObject(PageNumberValueObject.Create(1), PageNumberValueObject.Create(1));
        public static new PageNumberRangeValueObject Create(int valueStart, int valueEnd, bool validate = true) => Create(PageNumberValueObject.Create(valueStart, false), PageNumberValueObject.Create(valueEnd, false), validate);
    }
}
