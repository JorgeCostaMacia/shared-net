using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObject : IntRangeValueObject
    {
        public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static PageNumberRangeValueObject From(PageNumberValueObject valueStart, PageNumberValueObject valueEnd, bool validate = true)
        {
            PageNumberRangeValueObject ValueObject = new PageNumberRangeValueObject(valueStart, valueEnd);
            if (validate) new PageNumberRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberRangeValueObject From() => From(1, 1);
        public static new PageNumberRangeValueObject From(int valueStart, int valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
        public static new PageNumberRangeValueObject From(string valueStart, string valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
        public static new PageNumberRangeValueObject From(float valueStart, float valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
        public static new PageNumberRangeValueObject From(decimal valueStart, decimal valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
    }
}
