using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IpValueObjectValidatorTests
{
    private static readonly IpValueObjectValidator Validator = new(new StringValueObjectValidator());

    [Theory]
    [InlineData("192.168.0.1")]
    [InlineData("10.0.0.255")]
    [InlineData("255.255.255.255")]
    public void ValidIpv4_Passes(string value)
        => Assert.True(Validator.Validate(IpValueObject.Create(value)).IsValid);

    [Theory]
    [InlineData("")]                    // empty
    [InlineData("1.2.3")]               // too short / too few octets
    [InlineData("999.999.999.999")]     // out-of-range octets
    [InlineData("1.2.3.4.5")]           // too many octets
    [InlineData("not.an.ip.x")]         // not numeric
    public void InvalidIp_Fails(string value)
        => Assert.False(Validator.Validate(IpValueObject.Create(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<IpValueObjectValidationException>(() => Validator.ValidateAndThrow(IpValueObject.Create("999.999.999.999")));
}
