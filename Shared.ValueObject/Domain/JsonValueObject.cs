using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class JsonValueObject : StringValueObject
    {
        public JsonValueObject(string value) : base(value)
        {
        }

        public new static JsonValueObject From(string value, bool validate = true)
        {
            JsonValueObject ValueObject = new JsonValueObject(Convert(value));
            if (validate) new JsonValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public new static JsonValueObject From() => From("{}");

        public static new JsonValueObject? FromOrDefault(string? value, bool validate = true) => value != null && Convert(value) != "" ? From(value, validate) : From();
    }
}
