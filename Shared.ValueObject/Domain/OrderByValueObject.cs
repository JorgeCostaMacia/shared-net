using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class OrderByValueObject : StringValueObject
    {
        public OrderByValueObject(string value) : base(value)
        {
        }

        public static new OrderByValueObject Create(string value, bool validate = true)
        {
            OrderByValueObject ValueObject = new OrderByValueObject(ToValue(value));
            if (validate) new OrderByValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new OrderByValueObject Create() => new OrderByValueObject(string.Empty);
        public static new OrderByValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderByValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderByValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderByValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
        public static new OrderByValueObject Create(Guid value, bool validate = true) => Create(ToValue(value), validate);

        protected static new string ToValue(string value) => StringValueObject.ToValue(value).ToUpper();
    }
}