using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class GroupByValueObject : StringValueObject
    {
        public GroupByValueObject(string value) : base(value)
        {
        }

        public static new GroupByValueObject From(string value, bool validate = true)
        {
            GroupByValueObject ValueObject = new GroupByValueObject(Convert(value));
            if (validate) new GroupByValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
    }
}