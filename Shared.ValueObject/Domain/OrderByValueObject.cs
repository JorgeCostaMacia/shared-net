namespace Shared.ValueObject.Domain
{
    public class OrderByValueObject : StringValueObject
    {
        public OrderByValueObject() : base()
        {
        }

        public OrderByValueObject(string value) : base(value)
        {
        }

        public OrderByValueObject(int value) : base(value)
        {
        }

        public OrderByValueObject(float value) : base(value)
        {
        }

        public OrderByValueObject(bool value) : base(value)
        {
        }

        public OrderByValueObject(DateTime value) : base(value)
        {
        }
    }
}