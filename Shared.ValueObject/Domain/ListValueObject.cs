using FluentValidation;
using System.Collections.Immutable;

namespace Shared.ValueObject.Domain
{
    public class ListValueObject<T> : IValueObject
    {
        public ImmutableList<T> Value { get; }

        public ListValueObject(ImmutableList<T> value)
        {
            Value = value;
        }

        public static ListValueObject<T> Create(ImmutableList<T> value, bool validate = true)
        {
            ListValueObject<T> ValueObject = new ListValueObject<T>(ToValue(value));
            if (validate) new ListValueObjectValidator<T>().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static ListValueObject<T> Create() => new ListValueObject<T>(new List<T>().ToImmutableList());
        public static ListValueObject<T> Create(IEnumerable<T> value, bool validate = true) => Create(ToValue(value), validate);
        public static ListValueObject<T> Create(T value, bool validate = true) => Create(ToValue(value), validate);

        protected static ImmutableList<T> ToValue(ImmutableList<T> value) => value;
        protected static ImmutableList<T> ToValue(IEnumerable<T> value) => value.ToImmutableList<T>();
        protected static ImmutableList<T> ToValue(T value) => new List<T> { value }.ToImmutableList();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
