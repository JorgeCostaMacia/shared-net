using JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Support;

namespace JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Infrastructure;

[Collection(nameof(PostgreSqlCollection))]
public class EfConverterRoundTripIntegrationTests
{
    private readonly PostgreSqlFixture fixture;

    public EfConverterRoundTripIntegrationTests(PostgreSqlFixture fixture) => this.fixture = fixture;

    [Fact]
    public async Task EveryFamily_RoundTripsThroughRealPostgres()
    {
        CancellationToken cancellationToken = TestContext.Current.CancellationToken;
        Guid reference = new("11111111-1111-1111-1111-111111111111");

        await using (ProbeDbContext write = fixture.NewContext())
        {
            write.Add(new Sample
            {
                Id = new SampleId(1),
                Name = new SampleName("alice"),
                Nick = null,
                Ref = new SampleRef(reference),
                Flag = new SampleFlag(true),
                Count = new SampleCount(9_000_000_000L),
                Amount = new SampleAmount(123.45m),
                Ratio = new SampleRatio(1.5d),
                Weight = new SampleWeight(2.5f),
                Blob = new SampleBlob([1, 2, 3]),
                When = new SampleWhen(new DateTime(2026, 7, 12, 10, 30, 0, DateTimeKind.Unspecified)),
                WhenUtc = new SampleWhenUtc(new DateTime(2026, 7, 12, 10, 30, 0, DateTimeKind.Utc))
            });
            await write.SaveChangesAsync(cancellationToken);
        }

        // fresh context: the row comes back from the database, not from an identity map
        await using ProbeDbContext read = fixture.NewContext();
        Sample? loaded = await read.Samples.FindAsync([new SampleId(1)], cancellationToken);   // key lookup translates through the converter

        Assert.NotNull(loaded);
        Assert.Equal(1, loaded!.Id.Value);
        Assert.Equal("alice", loaded.Name.Value);
        Assert.Null(loaded.Nick);                                     // nullable value object: NULL column -> null reference
        Assert.Equal(reference, loaded.Ref.Value);
        Assert.True(loaded.Flag.Value);
        Assert.Equal(9_000_000_000L, loaded.Count.Value);
        Assert.Equal(123.45m, loaded.Amount.Value);
        Assert.Equal(1.5d, loaded.Ratio.Value);
        Assert.Equal(2.5f, loaded.Weight.Value);
        Assert.Equal(new byte[] { 1, 2, 3 }, loaded.Blob.Value);
    }
}
