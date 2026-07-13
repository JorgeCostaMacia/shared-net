using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class ValueObjectValidationExceptionHierarchyTests
{
    [Fact]
    public void DerivedException_IsAssignableToParent()
        => Assert.IsAssignableFrom<StringValueObjectValidationException>(
            new GroupByValueObjectValidationException(new List<ValidationFailure>()));

    [Fact]
    public void DerivedException_CarriesItsOwnCode_NotParents()
    {
        GroupByValueObjectValidationException group = new GroupByValueObjectValidationException(new List<ValidationFailure>());
        StringValueObjectValidationException @string = new StringValueObjectValidationException(new List<ValidationFailure>());

        Assert.NotEqual(@string.AggregateCode, group.AggregateCode);
    }

    [Fact]
    public void DateTimeUtcException_IsAssignableToDateTimeException()
        => Assert.IsAssignableFrom<DateTimeValueObjectValidationException>(
            new DateTimeUtcValueObjectValidationException(new List<ValidationFailure>()));

    [Fact]
    public void DateTimeUtcException_CarriesItsOwnCode_NotDateTimes()
    {
        DateTimeUtcValueObjectValidationException utc = new DateTimeUtcValueObjectValidationException(new List<ValidationFailure>());
        DateTimeValueObjectValidationException local = new DateTimeValueObjectValidationException(new List<ValidationFailure>());

        Assert.NotEqual(local.AggregateCode, utc.AggregateCode);
    }
}
