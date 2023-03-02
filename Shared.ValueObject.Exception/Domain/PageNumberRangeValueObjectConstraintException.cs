using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class PageNumberRangeValueObjectConstraintException : IConstraintException<int>
    {
        public PageNumberRangeValueObjectConstraintException(string property, int value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public PageNumberRangeValueObjectConstraintException(string property, int value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints) : base("PageNumberRangeValueObject", value, constraints, new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), "PageNumberRangeValueObject Constraint Exception")
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints, System.Exception inner) : base("PageNumberRangeValueObject", value, constraints, new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), "PageNumberRangeValueObject Constraint Exception", inner)
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints, string message) : base("PageNumberRangeValueObject", value, constraints, new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), message)
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints, string message, System.Exception inner) : base("PageNumberRangeValueObject", value, constraints, new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), message, inner)
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message) : base("PageNumberRangeValueObject", value, constraints, id, message)
        {
        }

        public PageNumberRangeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message, System.Exception inner) : base("PageNumberRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
