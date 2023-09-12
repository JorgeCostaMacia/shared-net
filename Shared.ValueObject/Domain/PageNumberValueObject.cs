using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberValueObject : IntValueObject
    {
        public PageNumberValueObject(int value) : base(value)
        {
        }

        public static new PageNumberValueObject Create(int value, bool validate = true)
        {
            PageNumberValueObject ValueObject = new PageNumberValueObject(ToValue(value));
            if (validate) new PageNumberValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberValueObject Create() => new PageNumberValueObject(1);
        public static new PageNumberValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageNumberValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageNumberValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageNumberValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
    }
}