using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;
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

    [Fact]
    public void PublicForwardingCtor_WithExplicitMetadata_AppliesIt()
    {
        Guid code = new Guid("0195f3d2-1a4b-7c8e-9f01-a2b3c4d5e6f7");
        BoolValueObjectValidationException exception = new BoolValueObjectValidationException(
            Guid.NewGuid(), "My.Custom.Type", code, 422, DateTime.UtcNow, "boom", null, new List<ValidationFailure>());

        Assert.Equal(code, exception.AggregateCode);
        Assert.Equal("My.Custom.Type", exception.AggregateType);
        Assert.Equal(422, exception.AggregateHttpCode);
    }

    [Fact]
    public void PublicForwardingCtor_WithNullMetadata_FallsBackToDefaults()
    {
        BoolValueObjectValidationException exception = new BoolValueObjectValidationException(
            null, null, null, null, null, null, null, new List<ValidationFailure>());

        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_HTTP_CODE, exception.AggregateHttpCode);
    }

    [Fact]
    public void DerivedException_PublicForwardingCtor_AppliesProvidedCode()
    {
        Guid code = new Guid("0195f3d2-2b5c-7d9f-a012-b3c4d5e6f708");
        GroupByValueObjectValidationException exception = new GroupByValueObjectValidationException(
            null, null, code, null, null, null, null, new List<ValidationFailure>());

        Assert.Equal(code, exception.AggregateCode);
    }
}
