namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObject : IValueObject
    {
        public float ValueStart { get; }
        public float ValueEnd { get; }

        public FloatRangeValueObject()
        {
            ValueStart = 0;
            ValueEnd = 0;
        }

        public FloatRangeValueObject(float valueStart, float valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public FloatRangeValueObject(int valueStart, int valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public FloatRangeValueObject(string valueStart, string valueEnd)
        {
            ValueStart = float.Parse(valueStart);
            ValueEnd = float.Parse(valueEnd);
        }

        public FloatRangeValueObject(bool valueStart, bool valueEnd)
        {
            ValueStart = valueStart ? 1 : 0;
            ValueEnd = valueEnd ? 1 : 0;
        }

        public FloatRangeValueObject(DateTime valueStart, DateTime valueEnd)
        {
            ValueStart = (float)new TimeSpan(valueStart.Ticks).TotalSeconds;
            ValueEnd = (float)new TimeSpan(valueEnd.Ticks).TotalSeconds;
        }
    }
}
