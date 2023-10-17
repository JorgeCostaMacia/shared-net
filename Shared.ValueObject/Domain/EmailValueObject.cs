using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class EmailValueObject : StringValueObject
    {
        public EmailValueObject(string value) : base(value)
        {
        }

        public static new EmailValueObject From(string value, bool validate = true)
        {
            EmailValueObject ValueObject = new EmailValueObject(Convert(value));
            if (validate) new EmailValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new EmailValueObject? FromOrDefault(string? value, bool validate = true) => value != null && Convert(value) != "" ? From(value, validate) : null;
    }
}