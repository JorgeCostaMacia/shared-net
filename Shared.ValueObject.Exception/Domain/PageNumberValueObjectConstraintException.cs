﻿using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class PageNumberValueObjectConstraintException : IConstraintException<PageNumberValueObject>
    {
        public PageNumberValueObjectConstraintException(string property, PageNumberValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public PageNumberValueObjectConstraintException(string property, PageNumberValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints) : base("PageNumberValueObject", value, constraints, new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), "PageNumberValueObject Constraint Exception")
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints, System.Exception inner) : base("PageNumberValueObject", value, constraints, new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), "PageNumberValueObject Constraint Exception", inner)
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints, string message) : base("PageNumberValueObject", value, constraints, new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), message)
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints, string message, System.Exception inner) : base("PageNumberValueObject", value, constraints, new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), message, inner)
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints, Guid id, string message) : base("PageNumberValueObject", value, constraints, id, message)
        {
        }

        public PageNumberValueObjectConstraintException(PageNumberValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("PageNumberValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
