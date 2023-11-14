using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageSizeValueObject : IntValueObject
    {
        public PageSizeValueObject(int value) : base(value)
        {
        }

        public static new PageSizeValueObject From(int value, bool validate = true)
        {
            PageSizeValueObject ValueObject = new PageSizeValueObject(Convert(value));
            if (validate) new PageSizeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageSizeValueObject From() => From(10000);
        public static new PageSizeValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static new PageSizeValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static new PageSizeValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);
    }
}