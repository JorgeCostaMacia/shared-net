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
        Assert.IsType<LongValueObjectConverter<ProbeCount>>(entity.FindProperty(nameof(ProbeEntity.Count))!.GetValueConverter());
        Assert.IsType<DecimalValueObjectConverter<ProbeAmount>>(entity.FindProperty(nameof(ProbeEntity.Amount))!.GetValueConverter());
        Assert.IsType<DoubleValueObjectConverter<ProbeRatio>>(entity.FindProperty(nameof(ProbeEntity.Ratio))!.GetValueConverter());
        Assert.IsType<FloatValueObjectConverter<ProbeWeight>>(entity.FindProperty(nameof(ProbeEntity.Weight))!.GetValueConverter());
        Assert.IsType<DateTimeValueObjectConverter<ProbeWhen>>(entity.FindProperty(nameof(ProbeEntity.When))!.GetValueConverter());
        Assert.IsType<ByteValueObjectConverter<ProbeBlob>>(entity.FindProperty(nameof(ProbeEntity.Blob))!.GetValueConverter());
    }

    [Fact]
    public void Convention_UsesTheSameConverter_ForNullableProperties()
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
        public ProbeCount Count { get; set; } = null!;
        public ProbeAmount Amount { get; set; } = null!;
        public ProbeRatio Ratio { get; set; } = null!;
        public ProbeWeight Weight { get; set; } = null!;
        public ProbeWhen When { get; set; } = null!;
        public ProbeBlob Blob { get; set; } = null!;
        public int PlainInt { get; set; }
        public string PlainText { get; set; } = null!;
    }

    public record ProbeId : IntValueObject { public ProbeId(int value) : base(value) { } }
    public record ProbeName : StringValueObject { public ProbeName(string value) : base(value) { } }
    public record ProbeRef : UuidValueObject { public ProbeRef(Guid value) : base(value) { } }
    public record ProbeFlag : BoolValueObject { public ProbeFlag(bool value) : base(value) { } }
    public record ProbeCount : LongValueObject { public ProbeCount(long value) : base(value) { } }
    public record ProbeAmount : DecimalValueObject { public ProbeAmount(decimal value) : base(value) { } }
    public record ProbeRatio : DoubleValueObject { public ProbeRatio(double value) : base(value) { } }
    public record ProbeWeight : FloatValueObject { public ProbeWeight(float value) : base(value) { } }
    public record ProbeWhen : DateTimeValueObject { public ProbeWhen(DateTime value) : base(value) { } }
    public record ProbeBlob : ByteValueObject { public ProbeBlob(byte[] value) : base(value) { } }

    // Multi-field value object: composed of inner single-value VOs, derives from no family base → skipped.
    public record ProbeRange(ProbeId Min, ProbeId Max);
}
