// 'GuidMySqlConverter' is referenced from its package namespace so the type isn't shadowed by
// the enclosing 'JorgeCostaMacia.GuidMySqlConverter' namespace in test code.
namespace JorgeCostaMacia.GuidMySqlConverter.Tests.Infrastructure;

public class GuidMySqlConverterTests
{
    [Fact]
    public void RoundTrip_ReturnsOriginalGuid()
    {
        Guid original = Guid.NewGuid();

        byte[] bytes = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(original);
        Guid roundTripped = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(bytes);

        Assert.Equal(original, roundTripped);
    }

    [Fact]
    public void ConvertToBytes_UsesBigEndianFirstFields()
    {
        Guid guid = new("00112233-4455-6677-8899-aabbccddeeff");

        byte[] converted = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(guid);

        // First three fields in big-endian (string) order, unlike .NET's little-endian ToByteArray().
        Assert.Equal(new byte[] { 0x00, 0x11, 0x22, 0x33 }, converted[..4]);
        Assert.Equal(new byte[] { 0x44, 0x55 }, converted[4..6]);
        Assert.Equal(new byte[] { 0x66, 0x77 }, converted[6..8]);
        Assert.NotEqual(guid.ToByteArray(), converted);
    }
}
