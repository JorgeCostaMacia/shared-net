using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IpValueObject : StringValueObject
    {
        public IpValueObject(string value) : base(value)
        {
        }

        public new static IpValueObject From(string value, bool validate = true)
        {
            IpValueObject ValueObject = new IpValueObject(Convert(value));
            if (validate) new IpValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public new static IpValueObject From() => From("0.0.0.0");

        public static new IpValueObject? FromOrDefault(string? value, bool validate = true) => value != null && Convert(value) != "" ? From(value, validate) : From();
    }
}
