using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class ListValueObjectConstraintException<T> : IConstraintException<List<T>>
    {
        public ListValueObjectConstraintException(List<T> value, List<string> constraints) : base("ListValueObject", value, constraints, new Guid("142884e2-5a8d-49d5-a4de-222b57b0ad09"), "ListValueObject Constraint Exception")
        {
        }

        public ListValueObjectConstraintException(List<T> value, List<string> constraints, System.Exception inner) : base("ListValueObject", value, constraints, new Guid("142884e2-5a8d-49d5-a4de-222b57b0ad09"), "ListValueObject Constraint Exception", inner)
        {
        }

        public ListValueObjectConstraintException(List<T> value, List<string> constraints, string message) : base("ListValueObject", value, constraints, new Guid("142884e2-5a8d-49d5-a4de-222b57b0ad09"), message)
        {
        }

        public ListValueObjectConstraintException(List<T> value, List<string> constraints, string message, System.Exception inner) : base("ListValueObject", value, constraints, new Guid("142884e2-5a8d-49d5-a4de-222b57b0ad09"), message, inner)
        {
        }

        public ListValueObjectConstraintException(List<T> value, List<string> constraints, Guid id, string message) : base("ListValueObject", value, constraints, id, message)
        {
        }

        public ListValueObjectConstraintException(List<T> value, List<string> constraints, Guid id, string message, System.Exception inner) : base("ListValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
