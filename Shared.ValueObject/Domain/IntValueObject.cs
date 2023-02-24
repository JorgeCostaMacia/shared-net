namespace Shared.ValueObject.Domain
{
    public class IntValueObject : IValueObject
    {
        public int Value { get; }

        public IntValueObject()
        {
            Value = 0;
        }

        public IntValueObject(int value)
        {
            Value = value;
        }

        public IntValueObject(float value)
        {
            Value = (int)value;
        }

        public IntValueObject(string value)
        {
            Value = int.Parse(value);
        }

        public IntValueObject(bool value)
        {
            Value = value ? 1 : 0;
        }

        public IntValueObject(DateTime value)
        {
            Value = (int)new TimeSpan(value.Ticks).TotalSeconds;
        }
    }
}
