using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class UrlValueObjectValidatorTests
{
    private static readonly UrlValueObjectValidator Validator = UrlValueObjectValidator.Create();

    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://a.b/c?d=e#f")]
    public void ValidHttpUrl_Passes(string value)
        => Assert.True(Validator.Validate(UrlValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]                 // empty
    [InlineData("notaurl")]          // not a URI
    [InlineData("/relative/path")]   // relative, not absolute
    public void InvalidUrl_Fails(string value)
        => Assert.False(Validator.Validate(UrlValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("javascript:alert(1)")]              // script scheme — stored-XSS vector if rendered
    [InlineData("data:text/html,<script></script>")] // data scheme
    [InlineData("file:///etc/passwd")]               // filesystem
    [InlineData("ftp://example.com/file")]           // valid URI, but not a WEB url — derive your own VO for other schemes
    public void NonHttpScheme_Fails(string value)
        => Assert.False(Validator.Validate(UrlValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<UrlValueObjectValidationException>(() => Validator.ValidateAndThrow(UrlValueObject.From("notaurl")));
}
