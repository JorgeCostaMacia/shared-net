namespace Shared.ValueObject.Domain
{
    public class GroupTypeValueObject : IntValueObject
    {
        public GroupTypeValueObject() : base(1)
        {
        }

        public GroupTypeValueObject(int value) : base(value)
        {
        }

        public GroupTypeValueObject(float value) : base(value)
        {
        }

        public GroupTypeValueObject(string value) : base(value)
        {
        }

        public GroupTypeValueObject(bool value) : base(value)
        {
        }

        public GroupTypeValueObject(DateTime value) : base(value)
        {
        }
    }
}