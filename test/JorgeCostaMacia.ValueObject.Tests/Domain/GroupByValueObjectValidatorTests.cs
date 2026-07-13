using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class GroupByValueObjectValidatorTests
{
    private static readonly GroupByValueObjectValidator _validator = GroupByValueObjectValidator.Create();

    [Theory]
    [InlineData("category")]
    [InlineData("country_code")]
    public void NonEmpty_Passes(string value)
        => Assert.True(_validator.Validate(GroupByValueObject.From(value)).IsValid);

    [Fact]
    public void Empty_Fails()
        => Assert.False(_validator.Validate(GroupByValueObject.From("")).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<GroupByValueObjectValidationException>(() => _validator.ValidateAndThrow(GroupByValueObject.From("")));
}
