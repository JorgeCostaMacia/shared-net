using JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Support;
using Microsoft.EntityFrameworkCore;

namespace JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Infrastructure;

[Collection(nameof(PostgreSqlCollection))]
public class DateTimeValueObjectMappingIntegrationTests
{
    private readonly PostgreSqlFixture _fixture;

    public DateTimeValueObjectMappingIntegrationTests(PostgreSqlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task PlainAndUtc_LandInTheirColumnTypes_AndPreserveKindOnRoundTrip()
    {
        CancellationToken cancellationToken = TestContext.Current.CancellationToken;
        DateTime plain = new DateTime(2026, 7, 12, 10, 30, 0, DateTimeKind.Unspecified);
        DateTime utc = new DateTime(2026, 7, 12, 10, 30, 0, DateTimeKind.Utc);

        await using (ProbeDbContext write = _fixture.NewContext())
        {
            write.Add(new Sample
            {
                Id = new SampleId(2),
                Name = new SampleName("bob"),
                Ref = new SampleRef(Guid.Empty),
                Flag = new SampleFlag(false),
                Count = new SampleCount(0),
                Amount = new SampleAmount(0m),
                Ratio = new SampleRatio(0d),
                Weight = new SampleWeight(0f),
                Blob = new SampleBlob(Array.Empty<byte>()),
                When = new SampleWhen(plain),
                WhenUtc = new SampleWhenUtc(utc)
            });
            await write.SaveChangesAsync(cancellationToken);
        }

        // the two time worlds really map to different Postgres column types
        await using ProbeDbContext read = _fixture.NewContext();
        Assert.Equal("timestamp without time zone", await ColumnTypeAsync(read, "When", cancellationToken));
        Assert.Equal("timestamp with time zone", await ColumnTypeAsync(read, "WhenUtc", cancellationToken));

        Sample loaded = (await read.Samples.FindAsync(new object[] { new SampleId(2) }, cancellationToken))!;

        // the plain stamp keeps its wall-clock value with no zone; the UTC stamp comes back tagged UTC
        Assert.Equal(plain, loaded.When.Value);
        Assert.Equal(DateTimeKind.Unspecified, loaded.When.Value.Kind);
        Assert.Equal(utc, loaded.WhenUtc.Value);
        Assert.Equal(DateTimeKind.Utc, loaded.WhenUtc.Value.Kind);
    }

    private static async Task<string?> ColumnTypeAsync(ProbeDbContext context, string column, CancellationToken cancellationToken)
    {
        await context.Database.OpenConnectionAsync(cancellationToken);

        try
        {
            await using System.Data.Common.DbCommand command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT data_type FROM information_schema.columns WHERE table_name = 'Samples' AND column_name = '{column}'";

            return (string?)await command.ExecuteScalarAsync(cancellationToken);
        }
        finally
        {
            await context.Database.CloseConnectionAsync();
        }
    }
}
