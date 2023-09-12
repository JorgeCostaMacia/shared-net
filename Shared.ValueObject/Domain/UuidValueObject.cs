using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class UuidValueObject : IValueObject
    {
        public Guid Value { get; }

        public UuidValueObject(Guid value)
        {
            Value = value;
        }

        public static UuidValueObject Create(Guid value, bool validate = true)
        {
            UuidValueObject ValueObject = new UuidValueObject(ToValue(value));
            if (validate) new UuidValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static UuidValueObject Create() => new UuidValueObject(Guid.NewGuid());
        public static UuidValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);

        protected static Guid ToValue(Guid value) => value;
        protected static Guid ToValue(string value) => new Guid(value.Trim());

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
