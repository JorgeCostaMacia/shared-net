namespace Shared.ValueObject.Domain
{
    public class ListValueObject<T> : IValueObject
    {
        public List<T> Value { get; }

        public ListValueObject()
        {
            Value = new List<T>();
        }

        public ListValueObject(List<T> value)
        {
            Value = value;
        }

        public ListValueObject(T value)
        {
            Value = new List<T>();
            Value.Add(value);
        }
    }
}
