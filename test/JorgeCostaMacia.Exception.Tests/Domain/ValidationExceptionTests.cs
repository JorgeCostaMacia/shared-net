using System.Collections.Immutable;
using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestValidationException(IEnumerable<ValidationFailure> validations, string? message = null)
    : ValidationException(null, null, null, null, null, message, null, validations);

file sealed class TestExplicitValidationException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException, ImmutableList<ValidationFailure> validations)
    : ValidationException(id, type, code, httpCode, occurredAt, message, innerException, validations);

public class ValidationExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestValidationException exception = new([]);

        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(400, exception.AggregateHttpCode);
    }

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

    [Fact]
    public void EmptyValidations_ProduceNoArrow()
    {
        TestValidationException exception = new([]);

        Assert.Equal($"{exception.AggregateId}/ValidationException", exception.Message);
    }

    [Fact]
    public void Validations_AreIsolatedFromTheSourceSequence()
    {
        List<ValidationFailure> source = [new("Name", "is required")];
        TestValidationException exception = new(source);

        source.Add(new("Age", "must be positive"));

        Assert.Single(exception.Validations);
    }

    [Fact]
    public void ExplicitCtor_StoresValidationsAndMetadata()
    {
        Guid id = Guid.NewGuid();
        ImmutableList<ValidationFailure> validations = [new("Name", "is required")];

        TestExplicitValidationException exception = new(id, "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", null, validations);

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal(validations, exception.Validations);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new("cause");

        TestExplicitValidationException exception = new(Guid.NewGuid(), "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", inner, []);

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Validations_WithNullPropertyName_FormatWithLeadingColon()
    {
        TestValidationException exception = new([new ValidationFailure(null, "is required")]);

        Assert.EndsWith("=> : is required", exception.Message);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestValidationException([]));
}
