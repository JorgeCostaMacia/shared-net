using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class GroupByValueObjectValidatorTests
{
    private static readonly GroupByValueObjectValidator Validator = new(new StringValueObjectValidator());

    [Theory]
    [InlineData("category")]
    [InlineData("country_code")]
    public void NonEmpty_Passes(string value)
        => Assert.True(Validator.Validate(GroupByValueObject.Create(value)).IsValid);

    [Fact]
    public void Empty_Fails()
        => Assert.False(Validator.Validate(GroupByValueObject.Create("")).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<GroupByValueObjectValidationException>(() => Validator.ValidateAndThrow(GroupByValueObject.Create("")));
}
