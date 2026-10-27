using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UriValueObjectValidatorTests
{
    private static readonly UriValueObjectValidator _validator = UriValueObjectValidator.Create();

    // The base takes any URI: absolute of any scheme, or relative. Narrowing it is a derived Value Object's job.
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://localhost:8080/path")]
    [InlineData("ftp://files.example.com")]
    [InlineData("urn:isbn:0451450523")]
    [InlineData("/rooted/path")]
    [InlineData("relative/path")]
    public void AnyUri_Passes(string value)
        => Assert.True(_validator.Validate(UriValueObject.From(new Uri(value, UriKind.RelativeOrAbsolute))).IsValid);

    [Fact]
    public void Null_Fails()
    {
        ValidationResult result = _validator.Validate(new UriValueObject(null!));

        Assert.Equal("NotNullValidator", Assert.Single(result.Errors).ErrorCode);
    }

    [Fact]
    public void Null_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<UriValueObjectValidationException>(
            () => _validator.ValidateAndThrow(new UriValueObject(null!)));

    // The derived Value Object narrows the rule: this is how an http(s)-only URL is built on the base.
    [Theory]
    [InlineData("https://example.com", true)]
    [InlineData("http://example.com/a", true)]
    [InlineData("ftp://example.com", false)]
    [InlineData("/rooted/path", false)]
    public void DerivedValidator_NarrowsToAWebUrl(
        string value,
        bool valid
    ) => Assert.Equal(
        valid,
        new WebUrlValidator().Validate(new WebUrl(new Uri(value, UriKind.RelativeOrAbsolute))).IsValid);

    public sealed record WebUrl : UriValueObject
    {
        public WebUrl(Uri value) : base(value) { }
    }

    public sealed class WebUrlValidator : AbstractValidator<WebUrl>
    {
        public WebUrlValidator()
        {
            Include(UriValueObjectValidator.Create());

            // IsAbsoluteUri first: Uri.Scheme throws on a relative URI.
            RuleFor(v => v.Value)
                .Must(v => v is not null
                    && v.IsAbsoluteUri
                    && (v.Scheme == Uri.UriSchemeHttp || v.Scheme == Uri.UriSchemeHttps))
                .WithErrorCode("WebUrlValidator")
                .WithMessage("{PropertyName} must be an http(s) Url");
        }
    }
}
