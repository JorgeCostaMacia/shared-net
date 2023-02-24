using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class PageSizeValueObjectConstraintException : IConstraintException<int>
    {
        public PageSizeValueObjectConstraintException(int value, List<string> constraints) : base("PageSizeValueObject", value, constraints, new Guid("b2b4f1e1-caae-4813-9ebd-ddc63c527cae"), "PageSizeValueObject Constraint Exception")
        {
        }

        public PageSizeValueObjectConstraintException(int value, List<string> constraints, System.Exception inner) : base("PageSizeValueObject", value, constraints, new Guid("b2b4f1e1-caae-4813-9ebd-ddc63c527cae"), "PageSizeValueObject Constraint Exception", inner)
        {
        }

        public PageSizeValueObjectConstraintException(int value, List<string> constraints, string message) : base("PageSizeValueObject", value, constraints, new Guid("b2b4f1e1-caae-4813-9ebd-ddc63c527cae"), message)
        {
        }

        public PageSizeValueObjectConstraintException(int value, List<string> constraints, string message, System.Exception inner) : base("PageSizeValueObject", value, constraints, new Guid("b2b4f1e1-caae-4813-9ebd-ddc63c527cae"), message, inner)
        {
        }

        public PageSizeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message) : base("PageSizeValueObject", value, constraints, id, message)
        {
        }

        public PageSizeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message, System.Exception inner) : base("PageSizeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
