using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class GroupByValueObject : StringValueObject
    {
        public GroupByValueObject(string value) : base(value)
        {
        }

        public static new GroupByValueObject Create(string value, bool validate = true)
        {
            GroupByValueObject ValueObject = new GroupByValueObject(ToValue(value));
            if (validate) new GroupByValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new GroupByValueObject Create() => new GroupByValueObject(string.Empty);
        public static new GroupByValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static new GroupByValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static new GroupByValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public static new GroupByValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
        public static new GroupByValueObject Create(Guid value, bool validate = true) => Create(ToValue(value), validate);

        protected static new string ToValue(string value) => StringValueObject.ToValue(value).ToUpper();
    }
}