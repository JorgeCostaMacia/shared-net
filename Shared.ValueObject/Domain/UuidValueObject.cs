namespace Shared.ValueObject.Domain
{
    public class UuidValueObject : IValueObject
    {
        public Guid Value { get; }

        public UuidValueObject()
        {
            Value = Guid.NewGuid();
        }

        public UuidValueObject(Guid value)
        {
            Value = value;
        }

        public UuidValueObject(string value)
        {
            Value = new Guid(value.Trim());
        }
    }
}
