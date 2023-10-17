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

        public static new GroupByValueObject? FromOrDefault(string? value, bool validate = true) => value != null && Convert(value) != "" ? From(value, validate) : null;

        protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
    }
}