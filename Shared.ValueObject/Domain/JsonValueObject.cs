using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class JsonValueObject : StringValueObject
    {
        public JsonValueObject(string value) : base(value)
        {
        }

        public new static JsonValueObject Create(string value, bool validate = true)
        {
            JsonValueObject ValueObject = new JsonValueObject(ToValue(value));
            if (validate) new JsonValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public new static JsonValueObject Create() => new JsonValueObject("{}");
        public new static JsonValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public new static JsonValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public new static JsonValueObject Create(bool value, bool validate = true) => Create(ToValue(value), validate);
        public new static JsonValueObject Create(DateTime value, bool validate = true) => Create(ToValue(value), validate);
        public new static JsonValueObject Create(Guid value, bool validate = true) => Create(ToValue(value), validate);

        protected new static string ToValue(string value) => value.Trim();
        protected new static string ToValue(int value) => "{value:" + value.ToString() + "}";
        protected new static string ToValue(float value) => "{value:" + value.ToString() + "}";
        protected new static string ToValue(bool value) => "{value:" + value.ToString() + "}";
        protected new static string ToValue(DateTime value) => "{value:" + value.ToString() + "}";
        protected new static string ToValue(Guid value) => "{value:" + value.ToString() + "}";
    }
}
