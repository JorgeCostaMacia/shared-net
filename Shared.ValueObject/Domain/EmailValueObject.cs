using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class EmailValueObject : StringValueObject
    {
        public EmailValueObject(string value) : base(value) { }

        public static EmailValueObject Create(string value, IValidator<EmailValueObject>? validator = null)
        {
            EmailValueObject ValueObject = new EmailValueObject(Convert(value));
            validator?.ValidateAndThrow(ValueObject);

            return ValueObject;
        }
    }
}