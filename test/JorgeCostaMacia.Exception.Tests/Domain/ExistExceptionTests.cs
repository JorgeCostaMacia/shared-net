using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestExistException(string? message = null)
    : ExistException(null, null, null, null, null, message, null);

file sealed class TestExplicitExistException(System.Exception? innerException)
    : ExistException(Guid.NewGuid(), "T", Guid.NewGuid(), 409, DateTime.UtcNow, "m", innerException);

public class ExistExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestExistException exception = new();

        Assert.NotEqual(Guid.Empty, exception.AggregateId);
        Assert.Equal(ExistExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ExistExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(409, exception.AggregateHttpCode);
    }

    [Fact]
    public void Message_IncludesIdAndType()
    {
        TestExistException exception = new("duplicate");

        Assert.Equal($"{exception.AggregateId}/ExistException => duplicate", exception.Message);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new("cause");

        TestExplicitExistException exception = new(inner);

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestExistException());
}
