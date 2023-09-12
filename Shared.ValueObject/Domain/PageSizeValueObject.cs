using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageSizeValueObject : IntValueObject
    {
        public PageSizeValueObject(int value) : base(value)
        {
        }

        public static new PageSizeValueObject Create(int value, bool validate = true)
        {
            PageSizeValueObject ValueObject = new PageSizeValueObject(ToValue(value));
            if (validate) new PageSizeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageSizeValueObject Create() => new PageSizeValueObject(10000);
        public static new PageSizeValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageSizeValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageSizeValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static new PageSizeValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
    }
}