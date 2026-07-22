using System.Collections.Immutable;
using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestValidationException(IEnumerable<ValidationFailure> validations, string? message = null)
    : ValidationException(null, null, null, null, null, message, null, validations);

file sealed class TestExplicitValidationException(
    Guid id, string type, Guid code, int httpCode, DateTime occurredAt, string? message, System.Exception? innerException, ImmutableList<ValidationFailure> validations)
    : ValidationException(id, type, code, httpCode, occurredAt, message, innerException, validations);

// Test Data Builder: defaults the metadata noise so each test states only the field it varies.
file sealed class ValidationExceptionBuilder
{
    private Guid _id = Guid.NewGuid();
    private ImmutableList<ValidationFailure> _validations = ImmutableList<ValidationFailure>.Empty;
    private System.Exception? _innerException = null;

    public ValidationExceptionBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ValidationExceptionBuilder WithValidations(ImmutableList<ValidationFailure> validations)
    {
        _validations = validations;
        return this;
    }

    public ValidationExceptionBuilder WithInnerException(System.Exception innerException)
    {
        _innerException = innerException;
        return this;
    }

    public TestExplicitValidationException Build()
        => new TestExplicitValidationException(_id, "T", Guid.NewGuid(), 400, DateTime.UtcNow, "m", _innerException, _validations);
}

public class ValidationExceptionTests
{
    [Fact]
    public void Defaults_AreApplied_WhenMetadataOmitted()
    {
        TestValidationException exception = new TestValidationException(Array.Empty<ValidationFailure>());

        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_TYPE, exception.AggregateType);
        Assert.Equal(ValidationExceptionDefaults.AGGREGATE_CODE, exception.AggregateCode);
        Assert.Equal(400, exception.AggregateHttpCode);
    }

    [Fact]
    public void Validations_AreStored_AndFormattedIntoMessage()
    {
        ValidationFailure[] failures = new ValidationFailure[]
        {
            new ValidationFailure("Name", "is required"),
            new ValidationFailure("Age", "must be positive"),
        };

        TestValidationException exception = new TestValidationException(failures);

        Assert.Equal(2, exception.Validations.Count);
        Assert.Equal("Name", exception.Validations[0].PropertyName);
        Assert.Equal($"{exception.AggregateId}/ValidationException => Name: is required; Age: must be positive", exception.Message);
    }

    [Fact]
    public void EmptyValidations_ProduceNoArrow()
    {
        TestValidationException exception = new TestValidationException(Array.Empty<ValidationFailure>());

        Assert.Equal($"{exception.AggregateId}/ValidationException", exception.Message);
    }

    [Fact]
    public void Validations_AreIsolatedFromTheSourceSequence()
    {
        List<ValidationFailure> source = new List<ValidationFailure> { new ValidationFailure("Name", "is required") };
        TestValidationException exception = new TestValidationException(source);

        source.Add(new ValidationFailure("Age", "must be positive"));

        Assert.Single(exception.Validations);
    }

    [Fact]
    public void ExplicitCtor_StoresValidationsAndMetadata()
    {
        Guid id = Guid.NewGuid();
        ImmutableList<ValidationFailure> validations = ImmutableList.Create(new ValidationFailure("Name", "is required"));

        TestExplicitValidationException exception = new ValidationExceptionBuilder().WithId(id).WithValidations(validations).Build();

        Assert.Equal(id, exception.AggregateId);
        Assert.Equal(validations, exception.Validations);
    }

    [Fact]
    public void InnerException_IsPropagated()
    {
        InvalidOperationException inner = new InvalidOperationException("cause");

        TestExplicitValidationException exception = new ValidationExceptionBuilder().WithInnerException(inner).Build();

        Assert.Same(inner, exception.InnerException);
    }

    [Fact]
    public void Validations_WithNullPropertyName_FormatWithLeadingColon()
    {
        TestValidationException exception = new TestValidationException(new ValidationFailure[] { new ValidationFailure(null, "is required") });

        Assert.EndsWith("=> : is required", exception.Message);
    }

    [Fact]
    public void Instance_IsAssignableToDomainException()
        => Assert.IsAssignableFrom<DomainException>(new TestValidationException(Array.Empty<ValidationFailure>()));
}
