using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;
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
/// A context that maps every value object explicitly, per property, and pins the two time worlds to
/// their column types: the plain <see cref="DateTimeValueObject"/> to <c>timestamp without time zone</c>
/// and the UTC one to <c>timestamp with time zone</c>.
/// </summary>
internal sealed class ProbeDbContext : DbContext
{
    public ProbeDbContext(DbContextOptions<ProbeDbContext> options) : base(options) { }

    public DbSet<Sample> Samples => Set<Sample>();

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.Entity<Sample>(entity =>
        {
            entity.HasKey(sample => sample.Id);
            entity.Property(sample => sample.Id).HasConversion(new IntValueObjectConverter<SampleId>()).ValueGeneratedNever();
            entity.Property(sample => sample.Name).HasConversion(new StringValueObjectConverter<SampleName>());
            entity.Property(sample => sample.Nick).HasConversion(typeof(StringValueObjectConverter<SampleName>));   // Type overload: the instance overload warns (CS8620) on a nullable property
            entity.Property(sample => sample.Ref).HasConversion(new UuidValueObjectConverter<SampleRef>());
            entity.Property(sample => sample.Flag).HasConversion(new BoolValueObjectConverter<SampleFlag>());
            entity.Property(sample => sample.Count).HasConversion(new LongValueObjectConverter<SampleCount>());
            entity.Property(sample => sample.Amount).HasConversion(new DecimalValueObjectConverter<SampleAmount>());
            entity.Property(sample => sample.Ratio).HasConversion(new DoubleValueObjectConverter<SampleRatio>());
            entity.Property(sample => sample.Weight).HasConversion(new FloatValueObjectConverter<SampleWeight>());
            entity.Property(sample => sample.Blob).HasConversion(new ByteValueObjectConverter<SampleBlob>());
            entity.Property(sample => sample.When).HasConversion(new DateTimeValueObjectConverter<SampleWhen>()).HasColumnType("timestamp without time zone");
            entity.Property(sample => sample.WhenUtc).HasConversion(new DateTimeValueObjectConverter<SampleWhenUtc>()).HasColumnType("timestamp with time zone");
        });
}
