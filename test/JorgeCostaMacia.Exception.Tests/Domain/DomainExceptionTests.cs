using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestDomainException(string? message = null, Guid? id = null)
    : DomainException(id, null, null, null, null, message, null);

public class DomainExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestDomainException exception = new();

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
        TestDomainException exception = new("boom");

        // The id embedded in the message must be the exact same value as AggregateId.
        Assert.Equal($"{exception.AggregateId}/DomainException => boom", exception.Message);
    }

    [Fact]
    public void Message_OmitsArrow_WhenNoMessage()
    {
        TestDomainException exception = new();

        Assert.Equal($"{exception.AggregateId}/DomainException", exception.Message);
    }

    [Fact]
    public void ExplicitId_IsPreserved()
    {
        Guid id = Guid.NewGuid();

        TestDomainException exception = new("x", id);

        Assert.Equal(id, exception.AggregateId);
        Assert.StartsWith($"{id}/DomainException", exception.Message);
    }
}
