using System.Collections.Immutable;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestErrorException(IEnumerable<string> errors, string? message = null)
    : ErrorException(null, null, null, null, null, message, null, errors);

file sealed class TestExplicitErrorException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException, ImmutableList<string> errors)
    : ErrorException(id, type, code, httpCode, occurredAt, message, innerException, errors);

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

    [Fact]
    public void ExplicitMessage_OverridesJoinedErrors()
    {
        TestErrorException exception = new(["a", "b"], "custom");

        Assert.EndsWith("=> custom", exception.Message);
        Assert.Equal(new[] { "a", "b" }, exception.Errors.ToArray());
    }

    [Fact]
    public void Errors_AreIsolatedFromTheSourceSequence()
    {
        List<string> source = ["a"];
        TestErrorException exception = new(source);

        source.Add("b");

        Assert.Single(exception.Errors);
    }

    [Fact]
    public void ExplicitCtor_StoresErrorsAndMetadata()
    {
        Guid id = Guid.NewGuid();
        ImmutableList<string> errors = ["x", "y"];

        TestExplicitErrorException exception = new(id, "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", null, errors);

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal(errors, exception.Errors);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new("cause");

        TestExplicitErrorException exception = new(Guid.NewGuid(), "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", inner, []);

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestErrorException(["e"]));
}
