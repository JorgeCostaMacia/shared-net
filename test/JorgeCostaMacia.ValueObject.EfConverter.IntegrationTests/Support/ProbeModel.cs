using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore;

namespace JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Support;

/// <summary>An entity carrying one value object per converter family, so a single round-trip exercises them all.</summary>
internal sealed class Sample
{
    public SampleId Id { get; set; } = null!;
    public SampleName Name { get; set; } = null!;
    public SampleName? Nick { get; set; }
    public SampleRef Ref { get; set; } = null!;
    public SampleFlag Flag { get; set; } = null!;
    public SampleCount Count { get; set; } = null!;
    public SampleAmount Amount { get; set; } = null!;
    public SampleRatio Ratio { get; set; } = null!;
    public SampleWeight Weight { get; set; } = null!;
    public SampleBlob Blob { get; set; } = null!;
    public SampleWhen When { get; set; } = null!;
    public SampleWhenUtc WhenUtc { get; set; } = null!;
}

internal record SampleId : IntValueObject { public SampleId(int value) : base(value) { } }
internal record SampleName : StringValueObject { public SampleName(string value) : base(value) { } }
internal record SampleRef : UuidValueObject { public SampleRef(Guid value) : base(value) { } }
internal record SampleFlag : BoolValueObject { public SampleFlag(bool value) : base(value) { } }
internal record SampleCount : LongValueObject { public SampleCount(long value) : base(value) { } }
internal record SampleAmount : DecimalValueObject { public SampleAmount(decimal value) : base(value) { } }
internal record SampleRatio : DoubleValueObject { public SampleRatio(double value) : base(value) { } }
internal record SampleWeight : FloatValueObject { public SampleWeight(float value) : base(value) { } }
internal record SampleBlob : ByteValueObject { public SampleBlob(byte[] value) : base(value) { } }
internal record SampleWhen : DateTimeValueObject { public SampleWhen(DateTime value) : base(value) { } }
internal record SampleWhenUtc : DateTimeUtcValueObject { public SampleWhenUtc(DateTime value) : base(value) { } }

/// <summary>
/// A context that maps every value object through the convention, and pins the two time worlds to
/// their column types: the plain <see cref="DateTimeValueObject"/> to <c>timestamp without time zone</c>
/// and the UTC one to <c>timestamp with time zone</c> (the convention gives both the same converter, so
/// the column type is the consumer's explicit choice).
/// </summary>
internal sealed class ProbeDbContext : DbContext
{
    public ProbeDbContext(DbContextOptions<ProbeDbContext> options) : base(options) { }

    public DbSet<Sample> Samples => Set<Sample>();

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        => builder.AddValueObjectConversions(typeof(ProbeDbContext).Assembly);

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.Entity<Sample>(entity =>
        {
            entity.HasKey(sample => sample.Id);
            entity.Property(sample => sample.Id).ValueGeneratedNever();
            entity.Property(sample => sample.When).HasColumnType("timestamp without time zone");
            entity.Property(sample => sample.WhenUtc).HasColumnType("timestamp with time zone");
        });
}
