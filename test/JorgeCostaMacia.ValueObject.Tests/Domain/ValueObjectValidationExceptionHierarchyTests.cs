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
        var group = new GroupByValueObjectValidationException(new List<ValidationFailure>());
        var @string = new StringValueObjectValidationException(new List<ValidationFailure>());

        Assert.NotEqual(@string.AggregateCode, group.AggregateCode);
    }

    [Fact]
    public void DateTimeUtcException_IsAssignableToDateTimeException()
        => Assert.IsAssignableFrom<DateTimeValueObjectValidationException>(
            new DateTimeUtcValueObjectValidationException(new List<ValidationFailure>()));

    [Fact]
    public void DateTimeUtcException_CarriesItsOwnCode_NotDateTimes()
    {
        var utc = new DateTimeUtcValueObjectValidationException(new List<ValidationFailure>());
        var local = new DateTimeValueObjectValidationException(new List<ValidationFailure>());

        Assert.NotEqual(local.AggregateCode, utc.AggregateCode);
    }
}
