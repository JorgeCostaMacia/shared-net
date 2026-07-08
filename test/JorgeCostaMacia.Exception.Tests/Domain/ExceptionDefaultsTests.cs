using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

// Cross-cutting invariants of the *Defaults classes (one distinct default code per exception type, etc.).
public class ExceptionDefaultsTests
{
    [Fact]
    public void EveryExceptionType_HasADistinctAggregateCode()
    {
        Guid[] codes =
        [
            DomainExceptionDefaults.AGGREGATE_CODE,
            ErrorExceptionDefaults.AGGREGATE_CODE,
            ExistExceptionDefaults.AGGREGATE_CODE,
            NotFoundExceptionDefaults.AGGREGATE_CODE,
            ValidationExceptionDefaults.AGGREGATE_CODE,
        ];

        Assert.Equal(codes.Length, codes.Distinct().Count());
    }

    [Fact]
    public void HttpCodes_MatchTheDocumentedValues()
    {
        Assert.Equal(500, DomainExceptionDefaults.AGGREGATE_HTTP_CODE);
        Assert.Equal(400, ErrorExceptionDefaults.AGGREGATE_HTTP_CODE);
        Assert.Equal(409, ExistExceptionDefaults.AGGREGATE_HTTP_CODE);
        Assert.Equal(404, NotFoundExceptionDefaults.AGGREGATE_HTTP_CODE);
        Assert.Equal(400, ValidationExceptionDefaults.AGGREGATE_HTTP_CODE);
    }

    [Fact]
    public void AggregateType_EqualsTheBaseTypeFullName()
    {
        Assert.Equal(typeof(DomainException).FullName, DomainExceptionDefaults.AGGREGATE_TYPE);
        Assert.Equal(typeof(ErrorException).FullName, ErrorExceptionDefaults.AGGREGATE_TYPE);
        Assert.Equal(typeof(ExistException).FullName, ExistExceptionDefaults.AGGREGATE_TYPE);
        Assert.Equal(typeof(NotFoundException).FullName, NotFoundExceptionDefaults.AGGREGATE_TYPE);
        Assert.Equal(typeof(ValidationException).FullName, ValidationExceptionDefaults.AGGREGATE_TYPE);
    }
}
