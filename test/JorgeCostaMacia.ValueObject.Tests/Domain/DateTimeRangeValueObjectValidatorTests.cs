using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeRangeValueObjectValidatorTests
{
    private static readonly DateTimeRangeValueObjectValidator _validator = DateTimeRangeValueObjectValidator.Create();

    private static readonly DateTime _early = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime _late = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void StartBeforeEnd_Passes()
        => Assert.True(_validator.Validate(DateTimeRangeValueObject.From(_early, _late)).IsValid);

    [Fact]
    public void StartEqualsEnd_Passes()
        => Assert.True(_validator.Validate(DateTimeRangeValueObject.From(_early, _early)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(_validator.Validate(DateTimeRangeValueObject.From(_late, _early)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeRangeValueObjectValidationException>(() => _validator.ValidateAndThrow(DateTimeRangeValueObject.From(_late, _early)));
}
