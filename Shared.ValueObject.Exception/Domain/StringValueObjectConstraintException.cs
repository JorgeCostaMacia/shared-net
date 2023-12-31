﻿using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class StringValueObjectConstraintException : IConstraintException<StringValueObject>
    {
        public StringValueObjectConstraintException(string property, StringValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public StringValueObjectConstraintException(string property, StringValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints) : base("StringValueObject", value, constraints, new Guid("1a66f23c-17f7-40bc-b92a-de79f9b095c3"), "StringValueObject Constraint Exception")
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints, System.Exception inner) : base("StringValueObject", value, constraints, new Guid("1a66f23c-17f7-40bc-b92a-de79f9b095c3"), "StringValueObject Constraint Exception", inner)
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints, string message) : base("StringValueObject", value, constraints, new Guid("1a66f23c-17f7-40bc-b92a-de79f9b095c3"), message)
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints, string message, System.Exception inner) : base("StringValueObject", value, constraints, new Guid("1a66f23c-17f7-40bc-b92a-de79f9b095c3"), message, inner)
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints, Guid id, string message) : base("StringValueObject", value, constraints, id, message)
        {
        }

        public StringValueObjectConstraintException(StringValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("StringValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
