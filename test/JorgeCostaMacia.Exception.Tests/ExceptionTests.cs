using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests;

// Concrete, test-only subclasses of the abstract exception types, exercising the
// "optional metadata" constructors (the ones that apply defaults).
file sealed class TestDomainException(string? message = null, Guid? id = null)
    : DomainException(id, null, null, null, null, message, null);

file sealed class TestErrorException(IEnumerable<string> errors, string? message = null)
    : ErrorException(null, null, null, null, null, message, null, errors);

file sealed class TestExistException(string? message = null)
    : ExistException(null, null, null, null, null, message, null);

file sealed class TestNotFoundException(string? message = null)
    : NotFoundException(null, null, null, null, null, message, null);

file sealed class TestValidationException(IEnumerable<ValidationFailure> validations, string? message = null)
    : ValidationException(null, null, null, null, null, message, null, validations);

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

public class HttpCodeDefaultsTests
{
    [Fact]
    public void Error_DefaultsTo_400() => Assert.Equal(400, new TestErrorException(["e"]).AggregateHttpCode);

    [Fact]
    public void Exist_DefaultsTo_409() => Assert.Equal(409, new TestExistException().AggregateHttpCode);

    [Fact]
    public void NotFound_DefaultsTo_404() => Assert.Equal(404, new TestNotFoundException().AggregateHttpCode);

    [Fact]
    public void Validation_DefaultsTo_400() => Assert.Equal(400, new TestValidationException([]).AggregateHttpCode);
}

public class ErrorExceptionTests
{
    [Fact]
    public void Errors_AreStored_AndJoinedIntoMessage()
    {
        TestErrorException exception = new(["first", "second"]);

        Assert.Equal(new[] { "first", "second" }, exception.Errors.ToArray());
        Assert.Equal($"{exception.AggregateId}/ErrorException => first; second", exception.Message);
    }
}

public class ValidationExceptionTests
{
    [Fact]
    public void Validations_AreStored_AndFormattedIntoMessage()
    {
        ValidationFailure[] failures =
        [
            new("Name", "is required"),
            new("Age", "must be positive"),
        ];

        TestValidationException exception = new(failures);

        Assert.Equal(2, exception.Validations.Count);
        Assert.Equal("Name", exception.Validations[0].PropertyName);
        Assert.Equal($"{exception.AggregateId}/ValidationException => Name: is required; Age: must be positive", exception.Message);
    }
}
