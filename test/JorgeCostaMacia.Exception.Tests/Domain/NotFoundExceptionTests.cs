using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestNotFoundException(string? message = null)
    : NotFoundException(null, null, null, null, null, message, null);

file sealed class TestExplicitNotFoundException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException)
    : NotFoundException(id, type, code, httpCode, occurredAt, message, innerException);

// Test Data Builder: defaults the metadata noise so each test states only the field it varies.
file sealed class NotFoundExceptionBuilder
{
    private System.Exception? _innerException = null;

    public NotFoundExceptionBuilder WithInnerException(System.Exception innerException)
    {
        _innerException = innerException;
        return this;
    }

    public TestExplicitNotFoundException Build()
        => new TestExplicitNotFoundException(Guid.NewGuid(), "T", Guid.NewGuid(), 404, DateTime.UtcNow, "m", _innerException);
}

public class NotFoundExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestNotFoundException exception = new TestNotFoundException();

        Assert.NotEqual(Guid.Empty, exception.AggregateId);
        Assert.Equal(NotFoundExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(NotFoundExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(404, exception.AggregateHttpCode);
    }

    [Fact]
    public void Message_IncludesIdAndType()
    {
        TestNotFoundException exception = new TestNotFoundException("missing");

        Assert.Equal($"{exception.AggregateId}/NotFoundException => missing", exception.Message);
    }

    [Fact]
    public void Message_OmitsArrow_WhenNoMessage()
    {
        TestNotFoundException exception = new TestNotFoundException();

        Assert.Equal($"{exception.AggregateId}/NotFoundException", exception.Message);
    }

    [Fact]
    public void ExplicitCtor_StoresAllMetadataVerbatim()
    {
        Guid id = Guid.NewGuid();
        Guid code = Guid.NewGuid();
        DateTime occurredAt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        TestExplicitNotFoundException exception = new TestExplicitNotFoundException(id, "My.Type", code, 404, occurredAt, "msg", null);

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal("My.Type", exception.AggregateType);
        Assert.Equal(code, exception.AggregateCode);
        Assert.Equal(404, exception.AggregateHttpCode);
        Assert.Equal(occurredAt, exception.AggregateOccurredAt);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new InvalidOperationException("cause");

        TestExplicitNotFoundException exception = new NotFoundExceptionBuilder().WithInnerException(inner).Build();

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestNotFoundException());
}
