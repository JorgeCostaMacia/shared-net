using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class EmailValueObject : StringValueObject
    {
        public EmailValueObject(string value) : base(value)
        {
        }

        public static new EmailValueObject Create(string value, bool validate = true)
        {
            EmailValueObject ValueObject = new EmailValueObject(ToValue(value));
            if (validate) new EmailValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new EmailValueObject Create() => new EmailValueObject(string.Empty);
    }
}