namespace Shared.ValueObject.Domain
{
    public class GroupByValueObject : StringValueObject
    {
        public GroupByValueObject() : base()
        {
        }

        public GroupByValueObject(string value) : base(value)
        {
        }

        public GroupByValueObject(int value) : base(value)
        {
        }

        public GroupByValueObject(float value) : base(value)
        {
        }

        public GroupByValueObject(bool value) : base(value)
        {
        }

        public GroupByValueObject(DateTime value) : base(value)
        {
        }
    }
}