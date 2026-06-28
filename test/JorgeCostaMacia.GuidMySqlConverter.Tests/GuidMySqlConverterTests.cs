// 'GuidMySqlConverter' is qualified with 'Infrastructure.' so the type isn't shadowed by the
// enclosing 'JorgeCostaMacia.GuidMySqlConverter' namespace in test code.
namespace JorgeCostaMacia.GuidMySqlConverter.Tests;

public class GuidMySqlConverterTests
{
    [Fact]
    public void RoundTrip_ReturnsOriginalGuid()
    {
        Guid original = Guid.NewGuid();

        byte[] bytes = Infrastructure.GuidMySqlConverter.ConvertToBytes(original);
        Guid roundTripped = Infrastructure.GuidMySqlConverter.ConvertFromBytes(bytes);

        Assert.Equal(original, roundTripped);
    }

    [Fact]
    public void ConvertToBytes_UsesBigEndianFirstFields()
    {
        Guid guid = new("00112233-4455-6677-8899-aabbccddeeff");

        byte[] converted = Infrastructure.GuidMySqlConverter.ConvertToBytes(guid);

        // First three fields in big-endian (string) order, unlike .NET's little-endian ToByteArray().
        Assert.Equal(new byte[] { 0x00, 0x11, 0x22, 0x33 }, converted[..4]);
        Assert.Equal(new byte[] { 0x44, 0x55 }, converted[4..6]);
        Assert.Equal(new byte[] { 0x66, 0x77 }, converted[6..8]);
        Assert.NotEqual(guid.ToByteArray(), converted);
    }
}
