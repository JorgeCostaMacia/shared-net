using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestNotFoundException(string? message = null)
    : NotFoundException(null, null, null, null, null, message, null);

public class NotFoundExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestNotFoundException exception = new();

        Assert.NotEqual(Guid.Empty, exception.AggregateId);
        Assert.Equal(NotFoundExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(NotFoundExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(404, exception.AggregateHttpCode);
    }

    [Fact]
    public void Message_IncludesIdAndType()
    {
        TestNotFoundException exception = new("missing");

        Assert.Equal($"{exception.AggregateId}/NotFoundException => missing", exception.Message);
    }
}
