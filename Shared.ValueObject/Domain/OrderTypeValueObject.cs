using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class OrderTypeValueObject : StringValueObject
    {
        public OrderTypeValueObject(string value) : base(value)
        {
        }

        public static new OrderTypeValueObject Create(string value, bool validate = true)
        {
            OrderTypeValueObject ValueObject = new OrderTypeValueObject(ToValue(value));
            if (validate) new OrderTypeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new OrderTypeValueObject Create() => new OrderTypeValueObject("ASC");
        public static new OrderTypeValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderTypeValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderTypeValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderTypeValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderTypeValueObject Create(Guid value, bool validate = true) => Create(ToValue(value), validate);

        protected static new string ToValue(string value) => StringValueObject.ToValue(value).ToUpper();
    }
}