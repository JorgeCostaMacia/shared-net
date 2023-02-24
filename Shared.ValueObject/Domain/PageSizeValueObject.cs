namespace Shared.ValueObject.Domain
{
    public class PageSizeValueObject : IntValueObject
    {
        public PageSizeValueObject() : base(10000)
        {
        }

        public PageSizeValueObject(int value) : base(value)
        {
        }

        public PageSizeValueObject(float value) : base(value)
        {
        }

        public PageSizeValueObject(string value) : base(value)
        {
        }

        public PageSizeValueObject(bool value) : base(value)
        {
        }

        public PageSizeValueObject(DateTime value) : base(value)
        {
        }
    }
}