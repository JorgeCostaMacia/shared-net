using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests;

public class ValueObjectConversionExtensionsTests
{
    private static IEntityType Entity()
    {
        using ProbeContext context = new();
        return context.Model.FindEntityType(typeof(ProbeEntity))!;
    }

    [Fact]
    public void Convention_AppliesTheFamilyConverter_ToEveryValueObjectProperty()
    {
        IEntityType entity = Entity();

        Assert.IsType<IntValueObjectConverter<ProbeId>>(entity.FindProperty(nameof(ProbeEntity.Id))!.GetValueConverter());
        Assert.IsType<StringValueObjectConverter<ProbeName>>(entity.FindProperty(nameof(ProbeEntity.Name))!.GetValueConverter());
        Assert.IsType<UuidValueObjectConverter<ProbeRef>>(entity.FindProperty(nameof(ProbeEntity.Ref))!.GetValueConverter());
        Assert.IsType<BoolValueObjectConverter<ProbeFlag>>(entity.FindProperty(nameof(ProbeEntity.Flag))!.GetValueConverter());
    }

    [Fact]
    public void Convention_UsesTheSameConverter_ForNullableProperties()
        // The nullable property shares the non-null converter (EF never passes null to it).
        => Assert.IsType<StringValueObjectConverter<ProbeName>>(Entity().FindProperty(nameof(ProbeEntity.Nick))!.GetValueConverter());

    [Fact]
    public void Convention_CoversValueObjectKeys()
        => Assert.IsType<IntValueObjectConverter<ProbeId>>(Entity().FindPrimaryKey()!.Properties[0].GetValueConverter());

    [Fact]
    public void Convention_LeavesPlainPrimitiveProperties_Untouched()
    {
        IEntityType entity = Entity();

        Assert.Null(entity.FindProperty(nameof(ProbeEntity.PlainInt))!.GetValueConverter());
        Assert.Null(entity.FindProperty(nameof(ProbeEntity.PlainText))!.GetValueConverter());
    }

    // The model builds even though the scanned assembly contains a multi-field value object
    // (ProbeRange, derived from no family base) — the convention skips it instead of throwing.
    private sealed class ProbeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("value-object-conversion-probe");

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
            => builder.AddValueObjectConversions(typeof(ProbeContext).Assembly);

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.Entity<ProbeEntity>().Property(e => e.Id).ValueGeneratedNever();
    }

    private sealed class ProbeEntity
    {
        public ProbeId Id { get; set; } = null!;
        public ProbeName Name { get; set; } = null!;
        public ProbeName? Nick { get; set; }
        public ProbeRef Ref { get; set; } = null!;
        public ProbeFlag Flag { get; set; } = null!;
        public int PlainInt { get; set; }
        public string PlainText { get; set; } = null!;
    }

    public record ProbeId : IntValueObject { public ProbeId(int value) : base(value) { } }
    public record ProbeName : StringValueObject { public ProbeName(string value) : base(value) { } }
    public record ProbeRef : UuidValueObject { public ProbeRef(Guid value) : base(value) { } }
    public record ProbeFlag : BoolValueObject { public ProbeFlag(bool value) : base(value) { } }

    // Multi-field value object: composed of inner single-value VOs, derives from no family base → skipped.
    public record ProbeRange(ProbeId Min, ProbeId Max);
}
