namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObject : IntRangeValueObject
    {
        public PageNumberRangeValueObject() : base(1, 1)
        {
        }

        public PageNumberRangeValueObject(int valueStart, int valueEnd) : base(valueStart, valueEnd)
        {
        }

        public PageNumberRangeValueObject(float valueStart, float valueEnd) : base(valueStart, valueEnd)
        {
        }

        public PageNumberRangeValueObject(string valueStart, string valueEnd) : base(valueStart, valueEnd)
        {
        }

        public PageNumberRangeValueObject(bool valueStart, bool valueEnd) : base(valueStart, valueEnd)
        {
        }

        public PageNumberRangeValueObject(DateTime valueStart, DateTime valueEnd) : base(valueStart, valueEnd)
        {
        }
    }
}
