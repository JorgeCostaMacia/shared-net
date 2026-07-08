// 'GuidMySqlConverter' is referenced from its package namespace ('GuidMySqlConverter.Infrastructure.')
// so the type isn't shadowed by the enclosing 'JorgeCostaMacia.GuidMySqlConverter' namespace in test code.
namespace JorgeCostaMacia.GuidMySqlConverter.Tests.Infrastructure;

public class GuidMySqlConverterTests
{
    private static readonly Guid KnownGuid = new("00112233-4455-6677-8899-aabbccddeeff");

    private static readonly byte[] KnownBytes =
        [0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff];

    [Fact]
    public void ConvertToBytes_KnownGuid_ProducesBigEndianLayout()
        => Assert.Equal(KnownBytes, GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(KnownGuid));

    [Fact]
    public void ConvertFromBytes_KnownBigEndianLayout_ReturnsGuid()
        => Assert.Equal(KnownGuid, GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(KnownBytes));

    [Fact]
    public void ConvertToBytes_LastEightBytes_AreUnchanged()
    {
        // Only the first three fields are reversed; bytes 8..15 must stay in .NET order.
        byte[] converted = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(KnownGuid);

        Assert.Equal(new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff }, converted[8..16]);
    }

    [Fact]
    public void ConvertToBytes_Empty_ReturnsAllZero()
        => Assert.Equal(new byte[16], GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(Guid.Empty));

    [Theory]
    [MemberData(nameof(BoundaryGuids))]
    public void RoundTrip_ReturnsOriginalGuid(Guid original)
    {
        byte[] bytes = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertToBytes(original);
        Guid roundTripped = GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(bytes);

        Assert.Equal(original, roundTripped);
    }

    [Fact]
    public void ConvertFromBytes_DoesNotMutateInput()
    {
        byte[] input = (byte[])KnownBytes.Clone();

        GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(input);

        Assert.Equal(KnownBytes, input);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(4)]
    [InlineData(15)]
    [InlineData(17)]
    public void ConvertFromBytes_WrongLength_Throws(int length)
        => Assert.Throws<ArgumentException>(() => GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(new byte[length]));

    [Fact]
    public void ConvertFromBytes_Null_Throws()
        => Assert.Throws<ArgumentNullException>(() => GuidMySqlConverter.Infrastructure.GuidMySqlConverter.ConvertFromBytes(null!));

    public static TheoryData<Guid> BoundaryGuids() =>
    [
        Guid.Empty,
        KnownGuid,
        new Guid([0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff]),
    ];
}
