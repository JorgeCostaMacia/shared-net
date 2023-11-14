using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class UrlValueObject : StringValueObject
    {
        public UrlValueObject(string value) : base(value)
        {
        }

        public static new UrlValueObject From(string value, bool validate = true)
        {
            UrlValueObject ValueObject = new UrlValueObject(Convert(value));
            if (validate) new UrlValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }
    }
}