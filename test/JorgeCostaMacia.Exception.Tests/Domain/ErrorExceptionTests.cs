using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestErrorException(IEnumerable<string> errors, string? message = null)
    : ErrorException(null, null, null, null, null, message, null, errors);

public class ErrorExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestErrorException exception = new(["e"]);

        Assert.Equal(ErrorExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ErrorExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(400, exception.AggregateHttpCode);
    }

    [Fact]
    public void Errors_AreStored_AndJoinedIntoMessage()
    {
        TestErrorException exception = new(["first", "second"]);

        Assert.Equal(new[] { "first", "second" }, exception.Errors.ToArray());
        Assert.Equal($"{exception.AggregateId}/ErrorException => first; second", exception.Message);
    }
}
