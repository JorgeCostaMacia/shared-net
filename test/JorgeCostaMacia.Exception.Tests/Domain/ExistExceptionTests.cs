using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestExistException(string? message = null)
    : ExistException(null, null, null, null, null, message, null);

file sealed class TestExplicitExistException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException)
    : ExistException(id, type, code, httpCode, occurredAt, message, innerException);

// Test Data Builder: defaults the metadata noise so each test states only the field it varies.
file sealed class ExistExceptionBuilder
{
    private System.Exception? _innerException = null;

    public ExistExceptionBuilder WithInnerException(System.Exception innerException)
    {
        _innerException = innerException;
        return this;
    }

    public TestExplicitExistException Build()
        => new TestExplicitExistException(Guid.NewGuid(), "T", Guid.NewGuid(), 409, DateTime.UtcNow, "m", _innerException);
}

public class ExistExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestExistException exception = new TestExistException();

        Assert.NotEqual(Guid.Empty, exception.AggregateId);
        Assert.Equal(ExistExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ExistExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(409, exception.AggregateHttpCode);
    }

    [Fact]
    public void Message_IncludesIdAndType()
    {
        TestExistException exception = new TestExistException("duplicate");

        Assert.Equal($"{exception.AggregateId}/ExistException => duplicate", exception.Message);
    }

    [Fact]
    public void Message_OmitsArrow_WhenNoMessage()
    {
        TestExistException exception = new TestExistException();

        Assert.Equal($"{exception.AggregateId}/ExistException", exception.Message);
    }

    [Fact]
    public void ExplicitCtor_StoresAllMetadataVerbatim()
    {
        Guid id = Guid.NewGuid();
        Guid code = Guid.NewGuid();
        DateTime occurredAt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        TestExplicitExistException exception = new TestExplicitExistException(id, "My.Type", code, 409, occurredAt, "msg", null);

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal("My.Type", exception.AggregateType);
        Assert.Equal(code, exception.AggregateCode);
        Assert.Equal(409, exception.AggregateHttpCode);
        Assert.Equal(occurredAt, exception.AggregateOccurredAt);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new InvalidOperationException("cause");

        TestExplicitExistException exception = new ExistExceptionBuilder().WithInnerException(inner).Build();

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestExistException());
}
