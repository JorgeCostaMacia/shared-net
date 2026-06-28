using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests;

file sealed class TestExistException(string? message = null)
    : ExistException(null, null, null, null, null, message, null);

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
}
