using System.Collections.Immutable;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestErrorException(IEnumerable<string> errors, string? message = null)
    : ErrorException(null, null, null, null, null, message, null, errors);

file sealed class TestExplicitErrorException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException, ImmutableList<string> errors)
    : ErrorException(id, type, code, httpCode, occurredAt, message, innerException, errors);

// Test Data Builder: defaults the metadata noise so each test states only the field it varies.
file sealed class ErrorExceptionBuilder
{
    private Guid _id = Guid.NewGuid();
    private ImmutableList<string> _errors = ImmutableList<string>.Empty;
    private System.Exception? _innerException = null;

    public ErrorExceptionBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ErrorExceptionBuilder WithErrors(ImmutableList<string> errors)
    {
        _errors = errors;
        return this;
    }

    public ErrorExceptionBuilder WithInnerException(System.Exception innerException)
    {
        _innerException = innerException;
        return this;
    }

    public TestExplicitErrorException Build()
        => new TestExplicitErrorException(_id, "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", _innerException, _errors);
}

public class ErrorExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestErrorException exception = new TestErrorException(new string[] { "e" });

        Assert.Equal(ErrorExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ErrorExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(400, exception.AggregateHttpCode);
    }

    [Fact]
    public void Errors_AreStored_AndJoinedIntoMessage()
    {
        TestErrorException exception = new TestErrorException(new string[] { "first", "second" });

        Assert.Equal(new[] { "first", "second" }, exception.Errors.ToArray());
        Assert.Equal($"{exception.AggregateId}/ErrorException => first; second", exception.Message);
    }

    [Fact]
    public void ExplicitMessage_OverridesJoinedErrors()
    {
        TestErrorException exception = new TestErrorException(new string[] { "a", "b" }, "custom");

        Assert.EndsWith("=> custom", exception.Message);
        Assert.Equal(new[] { "a", "b" }, exception.Errors.ToArray());
    }

    [Fact]
    public void Errors_AreIsolatedFromTheSourceSequence()
    {
        List<string> source = new List<string> { "a" };
        TestErrorException exception = new TestErrorException(source);

        source.Add("b");

        Assert.Single(exception.Errors);
    }

    [Fact]
    public void ExplicitCtor_StoresErrorsAndMetadata()
    {
        Guid id = Guid.NewGuid();
        ImmutableList<string> errors = ImmutableList.Create("x", "y");

        TestExplicitErrorException exception = new ErrorExceptionBuilder().WithId(id).WithErrors(errors).Build();

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal(errors, exception.Errors);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new InvalidOperationException("cause");

        TestExplicitErrorException exception = new ErrorExceptionBuilder().WithInnerException(inner).Build();

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void EmptyErrors_ProduceNoArrow()
    {
        TestErrorException exception = new TestErrorException(Array.Empty<string>());

        Assert.Equal($"{exception.AggregateId}/ErrorException", exception.Message);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestErrorException(new string[] { "e" }));
}
