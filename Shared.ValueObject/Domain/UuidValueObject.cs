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

        public override bool Equals(object? obj) => obj is UuidValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(UuidValueObject? left, UuidValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(UuidValueObject left, UuidValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
