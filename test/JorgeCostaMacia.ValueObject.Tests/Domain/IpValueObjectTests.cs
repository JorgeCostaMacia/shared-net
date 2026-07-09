using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IpValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  999.999.999.999  ", new IpValueObject("  999.999.999.999  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("192.168.1.1", IpValueObject.From("  192.168.1.1  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("999.999.999.999", IpValueObject.From("999.999.999.999").Value);

    [Fact]
    public void Create_Trims()
        => Assert.Equal("192.168.1.1", IpValueObject.Create("  192.168.1.1  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsIpValueObjectValidationException()
        => Assert.Throws<IpValueObjectValidationException>(() => IpValueObject.Create("999.999.999.999"));
}
