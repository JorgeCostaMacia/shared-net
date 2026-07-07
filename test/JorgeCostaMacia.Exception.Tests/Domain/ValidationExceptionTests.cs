using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.Exception.Tests.Domain;

file sealed class TestValidationException(IEnumerable<ValidationFailure> validations, string? message = null)
    : ValidationException(null, null, null, null, null, message, null, validations);

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
}
