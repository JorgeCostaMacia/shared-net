using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class OrderTypeValueObject : StringValueObject
    {
        public OrderTypeValueObject(string value) : base(value)
        {
        }

        public static new OrderTypeValueObject From(string value, bool validate = true)
        {
            OrderTypeValueObject ValueObject = new OrderTypeValueObject(Convert(value));
            if (validate) new OrderTypeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new OrderTypeValueObject From() => From("ASC");

        protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
    }
}