// 'GuidFactory' is referenced from its package namespace ('GuidFactory.Domain.') so the type isn't
// shadowed by the enclosing 'JorgeCostaMacia.GuidFactory' namespace in test code.
namespace JorgeCostaMacia.GuidFactory.Tests.Domain;

public class GuidFactoryTests
{
    [Fact]
    public void Create_ReturnsNonEmptyGuid() => Assert.NotEqual(Guid.Empty, GuidFactory.Domain.GuidFactory.Create());

    [Fact]
    public void Create_ReturnsUniqueValues()
    {
        int count = 1000;
        HashSet<Guid> ids = new HashSet<Guid>();

        for (int i = 0; i < count; i++)
        {
            Assert.True(ids.Add(GuidFactory.Domain.GuidFactory.Create()), "GuidFactory.Create() produced a duplicate.");
        }

        Assert.Equal(count, ids.Count);
    }

    [Fact]
    public void Create_HasRfc4122Variant()
    {
        // The variant lives in the two high bits of byte 8 of the RFC layout; RFC 4122 == '10'.
        byte variant = GuidFactory.Domain.GuidFactory.Create().ToByteArray()[8];

        Assert.Equal(0x80, variant & 0xC0);
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void Create_OnNet9OrGreater_ReturnsVersion7() => Assert.Equal(7, GuidFactory.Domain.GuidFactory.Create().Version);
#else
    [Fact]
    public void Create_OnNet8_ReturnsVersion4()
    {
        // Guid.Version is .NET 9+; on net8 read the version nibble (high 4 bits of byte 7 of the layout).
        int version = (GuidFactory.Domain.GuidFactory.Create().ToByteArray()[7] & 0xF0) >> 4;

        Assert.Equal(4, version);
    }
#endif
}
