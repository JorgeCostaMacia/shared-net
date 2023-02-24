namespace Shared.ValueObject.Domain
{
    public class PageNumberValueObject : IntValueObject
    {
        public PageNumberValueObject() : base(1)
        {
        }

        public PageNumberValueObject(int value) : base(value)
        {
        }

        public PageNumberValueObject(float value) : base(value)
        {
        }

        public PageNumberValueObject(string value) : base(value)
        {
        }

        public PageNumberValueObject(bool value) : base(value)
        {
        }

        public PageNumberValueObject(DateTime value) : base(value)
        {
        }
    }
}