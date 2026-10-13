using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UriValueObjectValidatorTests
{
    private static readonly UriValueObjectValidator _validator = UriValueObjectValidator.Create();

    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://localhost:8080/path")]
    [InlineData("ftp://files.example.com")]
    [InlineData("urn:isbn:0451450523")]
    public void AbsoluteUri_Passes(string value)
        => Assert.True(_validator.Validate(UriValueObject.From(new Uri(value))).IsValid);

    [Theory]
    [InlineData("relative/path")]
    [InlineData("/rooted/path")]
    public void RelativeUri_Fails(string value)
    {
        ValidationResult result = _validator.Validate(UriValueObject.From(new Uri(value, UriKind.Relative)));

        Assert.Equal("UriValidator", Assert.Single(result.Errors).ErrorCode);
    }

    // No cascade: a missing value reports both what it is (null) and what it is not (an absolute Uri).
    [Fact]
    public void Null_ReportsBothRules()
    {
        ValidationResult result = _validator.Validate(new UriValueObject(null!));

        Assert.Equal(2, result.Errors.Count);
    }

    // The derived Value Object narrows the rule: this is how an http(s)-only URL is built on the base.
    [Fact]
    public void DerivedValidator_CanNarrowTheScheme()
    {
        HttpUriValidator validator = new HttpUriValidator();

        Assert.True(validator.Validate(new HttpUri(new Uri("https://example.com"))).IsValid);
        Assert.False(validator.Validate(new HttpUri(new Uri("ftp://example.com"))).IsValid);
    }

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<UriValueObjectValidationException>(() => _validator.ValidateAndThrow(UriValueObject.From(new Uri("relative", UriKind.Relative))));

    public sealed record HttpUri : UriValueObject
    {
        public HttpUri(Uri value) : base(value) { }
    }

    public sealed class HttpUriValidator : AbstractValidator<HttpUri>
    {
        public HttpUriValidator()
        {
            Include(UriValueObjectValidator.Create());

            RuleFor(v => v.Value)
                .Must(v => v.Scheme == Uri.UriSchemeHttp || v.Scheme == Uri.UriSchemeHttps)
                .WithErrorCode("HttpUriValidator")
                .WithMessage("{PropertyName} must be an http(s) Uri");
        }
    }
}
