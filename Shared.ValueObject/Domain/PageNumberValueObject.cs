using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberValueObject : IntValueObject
    {
        public PageNumberValueObject(int value) : base(value)
        {
        }

        public static new PageNumberValueObject From(int value, bool validate = true)
        {
            PageNumberValueObject ValueObject = new PageNumberValueObject(Convert(value));
            if (validate) new PageNumberValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberValueObject From() => From(1);
        public static new PageNumberValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static new PageNumberValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static new PageNumberValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);
    }
}