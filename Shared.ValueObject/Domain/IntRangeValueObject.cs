namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObject : IValueObject
    {
        public int ValueStart { get; }
        public int ValueEnd { get; }

        public IntRangeValueObject()
        {
            ValueStart = 0;
            ValueEnd = 0;
        }

        public IntRangeValueObject(int valueStart, int valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public IntRangeValueObject(float valueStart, float valueEnd)
        {
            ValueStart = (int)valueStart;
            ValueEnd = (int)valueEnd;
        }

        public IntRangeValueObject(string valueStart, string valueEnd)
        {
            ValueStart = int.Parse(valueStart);
            ValueEnd = int.Parse(valueEnd);
        }

        public IntRangeValueObject(bool valueStart, bool valueEnd)
        {
            ValueStart = valueStart ? 1 : 0;
            ValueEnd = valueEnd ? 1 : 0;
        }

        public IntRangeValueObject(DateTime valueStart, DateTime valueEnd)
        {
            ValueStart = (int)new TimeSpan(valueStart.Ticks).TotalSeconds;
            ValueEnd = (int)new TimeSpan(valueEnd.Ticks).TotalSeconds;
        }
    }
}
