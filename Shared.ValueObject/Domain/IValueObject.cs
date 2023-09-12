namespace Shared.ValueObject.Domain
{
    public abstract class IValueObject
    {
        protected static bool EqualOperator(IValueObject left, IValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, right) || (!ReferenceEquals(left, null) && left.Equals(right));
        }

        protected static bool NotEqualOperator(IValueObject left, IValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (IValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(IValueObject one, IValueObject two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(IValueObject one, IValueObject two)
        {
            return NotEqualOperator(one, two);
        }
    }
}
