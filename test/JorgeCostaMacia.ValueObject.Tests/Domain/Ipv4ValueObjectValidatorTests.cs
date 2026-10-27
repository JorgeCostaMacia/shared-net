using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class Ipv4ValueObjectValidatorTests
{
    private static readonly Ipv4ValueObjectValidator _validator = Ipv4ValueObjectValidator.Create();

    [Theory]
    [InlineData("192.168.0.1")]
    [InlineData("10.0.0.255")]
    [InlineData("255.255.255.255")]
    [InlineData("0.0.0.0")]
    public void CanonicalIpv4_Passes(string value)
        => Assert.True(_validator.Validate(Ipv4ValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]                    // empty
    [InlineData("1")]                   // inet_addr: 0.0.0.1
    [InlineData("1.2.3")]               // inet_addr: 1.2.0.3
    [InlineData("4294967295")]          // inet_addr: 255.255.255.255
    [InlineData("1.2.3.4.5")]           // too many octets
    [InlineData("999.1.1.1")]           // out-of-range octet
    [InlineData("010.0.0.1")]           // a leading zero reads as octal: 8.0.0.1
    [InlineData("01.02.03.04")]         // leading zeros
    [InlineData("0x1.0x2.0x3.0x4")]     // hexadecimal octets
    [InlineData("::1")]                 // IPv6
    [InlineData("not.an.ip.x")]         // not numeric
    public void NonCanonicalOrNotIpv4_Fails(string value)
        => Assert.False(_validator.Validate(Ipv4ValueObject.From(value)).IsValid);

    // No cascade: an empty value reports both what it is (empty) and what it is not (an IPv4).
    [Fact]
    public void Empty_ReportsBothRules()
    {
        ValidationResult result = _validator.Validate(Ipv4ValueObject.From(""));

        Assert.Equal(2, result.Errors.Count);
        Assert.Contains(result.Errors, e => e.ErrorCode == "Ipv4Validator");
    }

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<Ipv4ValueObjectValidationException>(
            () => _validator.ValidateAndThrow(Ipv4ValueObject.From("010.0.0.1")));
}
