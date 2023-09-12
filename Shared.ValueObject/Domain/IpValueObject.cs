using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IpValueObject : StringValueObject
    {
        public IpValueObject(string value) : base(value)
        {
        }

        public new static IpValueObject Create(string value, bool validate = true)
        {
            IpValueObject ValueObject = new IpValueObject(ToValue(value));
            if (validate) new IpValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public new static IpValueObject Create() => new IpValueObject("0.0.0.0");

        protected new static string ToValue(string value) => value.Trim();
    }
}
