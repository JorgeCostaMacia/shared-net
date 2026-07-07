// 'GuidFactory' is referenced fully-qualified (global::) so the type isn't shadowed by
// the enclosing 'JorgeCostaMacia.GuidFactory' namespace in test code.
namespace JorgeCostaMacia.GuidFactory.Tests.Domain;

public class GuidFactoryTests
{
    [Fact]
    public void Create_ReturnsNonEmptyGuid()
    {
        Assert.NotEqual(Guid.Empty, global::JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create());
    }

    [Fact]
    public void Create_ReturnsUniqueValues()
    {
        int count = 1000;
        HashSet<Guid> ids = new();

        for (int i = 0; i < count; i++)
        {
            Assert.True(ids.Add(global::JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create()), "GuidFactory.Create() produced a duplicate.");
        }

        Assert.Equal(count, ids.Count);
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void Create_OnNet9OrGreater_ReturnsVersion7()
    {
        Assert.Equal(7, global::JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create().Version);
    }
#endif
}
