namespace Shared.ValueObject.Domain
{
    public class OrderTypeValueObject : IntValueObject
    {
        public OrderTypeValueObject() : base(1)
        {
        }

        public OrderTypeValueObject(int value) : base(value)
        {
        }

        public OrderTypeValueObject(float value) : base(value)
        {
        }

        public OrderTypeValueObject(string value) : base(value)
        {
        }

        public OrderTypeValueObject(bool value) : base(value)
        {
        }

        public OrderTypeValueObject(DateTime value) : base(value)
        {
        }
    }
}