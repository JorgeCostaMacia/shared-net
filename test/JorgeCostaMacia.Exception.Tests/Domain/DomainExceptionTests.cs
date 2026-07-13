using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestDomainException(string? message = null, Guid? id = null, string? type = null)
    : DomainException(id, type, null, null, null, message, null);

file sealed class TestExplicitDomainException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException)
    : DomainException(id, type, code, httpCode, occurredAt, message, innerException);

public class DomainExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestDomainException exception = new TestDomainException();

        Assert.NotEqual(Guid.Empty, exception.AggregateId);
        Assert.Equal(DomainExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(DomainExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(500, exception.AggregateHttpCode);
        Assert.True(exception.AggregateOccurredAt <= DateTime.UtcNow);
        Assert.True(exception.AggregateOccurredAt > DateTime.UtcNow.AddMinutes(-1));
    }

    [Fact]
    public void Message_IncludesIdAndType_AndReusesTheSameId()
    {
        TestDomainException exception = new TestDomainException("boom");

        // The id embedded in the message must be the exact same value as AggregateId.
        Assert.Equal($"{exception.AggregateId}/DomainException => boom", exception.Message);
    }

    [Fact]
    public void Message_OmitsArrow_WhenNoMessage()
    {
        TestDomainException exception = new TestDomainException();

        Assert.Equal($"{exception.AggregateId}/DomainException", exception.Message);
    }

    [Fact]
    public void Message_TrimsSuppliedMessage()
    {
        TestDomainException exception = new TestDomainException("   boom   ");

        Assert.Equal($"{exception.AggregateId}/DomainException => boom", exception.Message);
    }

    [Fact]
    public void ExplicitId_IsPreserved()
    {
        Guid id = Guid.NewGuid();

        TestDomainException exception = new TestDomainException("x", id);

        Assert.Equal(id, exception.AggregateId);
        Assert.StartsWith($"{id}/DomainException", exception.Message);
    }

    [Fact]
    public void ExplicitCtor_StoresAllMetadataVerbatim()
    {
        Guid id = Guid.NewGuid();
        Guid code = Guid.NewGuid();
        DateTime occurredAt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        TestExplicitDomainException exception = new TestExplicitDomainException(id, "My.Custom.Type", code, 418, occurredAt, "msg", null);

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal("My.Custom.Type", exception.AggregateType);
        Assert.Equal(code, exception.AggregateCode);
        Assert.Equal(418, exception.AggregateHttpCode);
        Assert.Equal(occurredAt, exception.AggregateOccurredAt);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new InvalidOperationException("cause");

        TestExplicitDomainException exception = new TestExplicitDomainException(Guid.NewGuid(), "T", Guid.NewGuid(), 500, DateTime.UtcNow, "m", inner);

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Instance_IsAssignableToSystemException()
        => Assert.IsAssignableFrom<System.Exception>(new TestDomainException());

    [Fact]
    public void Message_UsesLastSegmentOfDottedType()
    {
        TestDomainException exception = new TestDomainException("boom", type: "A.B.C.Widget");

        Assert.Equal($"{exception.AggregateId}/Widget => boom", exception.Message);
    }

    [Fact]
    public void Message_WhitespaceOnly_KeepsArrowWithEmptyTail()
    {
        // Documents the current asymmetry: "" omits the arrow, but whitespace is not empty so a trailing "=> " remains.
        TestDomainException exception = new TestDomainException("   ");

        Assert.Equal($"{exception.AggregateId}/DomainException => ", exception.Message);
    }

    [Fact]
    public void ExplicitCtor_Message_IsRawAndUnprefixed()
    {
        TestExplicitDomainException exception = new TestExplicitDomainException(Guid.NewGuid(), "T", Guid.NewGuid(), 500, DateTime.UtcNow, "raw message", null);

        Assert.Equal("raw message", exception.Message);
    }
}
