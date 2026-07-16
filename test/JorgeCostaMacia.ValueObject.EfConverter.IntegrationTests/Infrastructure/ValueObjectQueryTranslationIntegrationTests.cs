using JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Support;
using Microsoft.EntityFrameworkCore;

namespace JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Infrastructure;

/// <summary>
/// Pins the query-translation contract of the value-object converters against real Postgres: filtering,
/// ordering and string matching over a value-object property translate to SQL <b>as long as the query
/// uses the value object itself or its implicit cast to the primitive</b> — equality, null checks over a
/// nullable VO, order comparisons (<c>&gt;</c>/<c>&lt;</c>), <c>Contains</c>/<c>StartsWith</c>/<c>EndsWith</c>
/// via the cast, and <c>OrderBy</c>. Reaching through <c>.Value</c> instead breaks translation (EF sees an
/// unmapped member of the model type, not the column), so those forms throw and are documented here as the
/// rule to avoid. Behaviour verified identical on EF Core 9 and 10.
/// </summary>
[Collection(nameof(PostgreSqlCollection))]
public class ValueObjectQueryTranslationIntegrationTests
{
    private const string Prefix = "contract-";

    private readonly PostgreSqlFixture _fixture;

    public ValueObjectQueryTranslationIntegrationTests(PostgreSqlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ValueObjectQueries_ThroughTheVoOrItsImplicitCast_TranslateToSql_WhileDotValueDoesNot()
    {
        CancellationToken cancellationToken = TestContext.Current.CancellationToken;

        await using (ProbeDbContext write = _fixture.NewContext())
        {
            write.Add(NewSample(200, Prefix + "alice", Prefix + "nick", 50L, 500m));
            write.Add(NewSample(201, Prefix + "bob", null, 150L, 1500m));
            write.Add(NewSample(202, Prefix + "alice-two", Prefix + "other", 250L, 2500m));
            await write.SaveChangesAsync(cancellationToken);
        }

        await using ProbeDbContext read = _fixture.NewContext();

        // --- these translate to SQL: work with the VO, or cast it to its primitive ---

        // equality over a value object
        Assert.Equal(1, await read.Samples.CountAsync(s => s.Name == new SampleName(Prefix + "alice"), cancellationToken));

        // null / not-null over a nullable value object
        Assert.Equal(1, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && s.Nick == null, cancellationToken));
        Assert.Equal(2, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && s.Nick != null, cancellationToken));

        // order comparisons, comparing value object to value object (implicit operator)
        Assert.Equal(2, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && s.Count > new SampleCount(100L), cancellationToken));
        Assert.Equal(2, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && s.Amount < new SampleAmount(2000m), cancellationToken));

        // string matching via the cast to the primitive
        Assert.Equal(3, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix), cancellationToken));
        Assert.Equal(2, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && ((string)s.Name).Contains("alice"), cancellationToken));
        Assert.Equal(1, await read.Samples.CountAsync(s => ((string)s.Name).EndsWith("-two"), cancellationToken));

        // nullable string matching: null guard + cast
        Assert.Equal(1, await read.Samples.CountAsync(s => ((string)s.Name).StartsWith(Prefix) && s.Nick != null && ((string)s.Nick).Contains("nick"), cancellationToken));

        // ordering by a value object
        List<Sample> ordered = await read.Samples.Where(s => ((string)s.Name).StartsWith(Prefix)).OrderBy(s => s.Count).ToListAsync(cancellationToken);
        Assert.Equal(50L, ordered[0].Count.Value);
        Assert.Equal(250L, ordered[^1].Count.Value);

        // --- these do NOT translate: reaching through .Value throws (the rule to avoid) ---

        await Assert.ThrowsAsync<InvalidOperationException>(() => read.Samples.Where(s => s.Count.Value > 100).ToListAsync(cancellationToken));
        await Assert.ThrowsAsync<InvalidOperationException>(() => read.Samples.Where(s => s.Name.Value.Contains("alice")).ToListAsync(cancellationToken));
        await Assert.ThrowsAsync<InvalidOperationException>(() => read.Samples.Where(s => EF.Functions.Like(s.Name.Value, "%alice%")).ToListAsync(cancellationToken));
        await Assert.ThrowsAsync<InvalidOperationException>(() => read.Samples.OrderBy(s => s.Count.Value).ToListAsync(cancellationToken));
    }

    private static Sample NewSample(int id, string name, string? nick, long count, decimal amount)
        => new Sample
        {
            Id = new SampleId(id),
            Name = new SampleName(name),
            Nick = nick is null ? null : new SampleName(nick),
            Ref = new SampleRef(Guid.NewGuid()),
            Flag = new SampleFlag(true),
            Count = new SampleCount(count),
            Amount = new SampleAmount(amount),
            Ratio = new SampleRatio(1.0d),
            Weight = new SampleWeight(1.0f),
            Blob = new SampleBlob(new byte[] { 1 }),
            When = new SampleWhen(new DateTime(2026, 7, 14, 10, 0, 0, DateTimeKind.Unspecified)),
            WhenUtc = new SampleWhenUtc(new DateTime(2026, 7, 14, 10, 0, 0, DateTimeKind.Utc))
        };
}
