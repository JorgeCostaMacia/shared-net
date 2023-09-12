using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class ByteValueObject : IValueObject
    {
        public byte[] Value { get; }

        public ByteValueObject(byte[] value)
        {
            Value = value;
        }

        public static ByteValueObject Create(byte[] value, bool validate = true)
        {
            ByteValueObject ValueObject = new ByteValueObject(ToValue(value));
            if (validate) new ByteValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static ByteValueObject Create() => new ByteValueObject(Array.Empty<byte>());
        public static ByteValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);

        protected static byte[] ToValue(byte[] value) => value;
        protected static byte[] ToValue(string value) => Convert.FromBase64String(value.Trim());

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
