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

        public static new OrderTypeValueObject? FromOrDefault(string? value, bool validate = true) => value != null && Convert(value) != "" ? From(value, validate) : From();

        protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
    }
}