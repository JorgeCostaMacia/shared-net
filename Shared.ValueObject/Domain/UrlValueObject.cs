using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class UrlValueObject : StringValueObject
    {
        public UrlValueObject(string value) : base(value)
        {
        }

        public static new UrlValueObject Create(string value, bool validate = true)
        {
            UrlValueObject ValueObject = new UrlValueObject(ToValue(value));
            if (validate) new UrlValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new UrlValueObject Create() => new UrlValueObject("");
    }
}