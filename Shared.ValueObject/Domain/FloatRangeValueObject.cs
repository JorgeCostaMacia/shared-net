namespace Shared.ValueObject.Domain
{
    public class FloatValueObject : IValueObject
    {
        public float Value { get; }

        public FloatValueObject()
        {
            Value = 0;
        }

        public FloatValueObject(float value)
        {
            Value = value;
        }

        public FloatValueObject(int value)
        {
            Value = value;
        }

        public FloatValueObject(string value)
        {
            Value = float.Parse(value);
        }

        public FloatValueObject(bool value)
        {
            Value = value ? 1 : 0;
        }

        public FloatValueObject(DateTime value)
        {
            Value = (float)new TimeSpan(value.Ticks).TotalSeconds;
        }
    }
}
