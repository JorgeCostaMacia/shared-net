using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace JorgeCostaMacia.ValueObject.EfConverter.IntegrationTests.Support;

/// <summary>
/// A throwaway PostgreSQL container shared across the integration tests. It builds the schema once, so
/// the converters are exercised against a real relational engine — the value converters actually fire,
/// SQL is generated, and the round-trip goes through real columns (which EF Core InMemory bypasses).
/// </summary>
public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer container = new PostgreSqlBuilder("postgres:16").Build();

    public async ValueTask InitializeAsync()
    {
        await container.StartAsync(TestContext.Current.CancellationToken);

        await using ProbeDbContext context = NewContext();
        await context.Database.EnsureCreatedAsync(TestContext.Current.CancellationToken);
    }

    public async ValueTask DisposeAsync() => await container.DisposeAsync();

    /// <summary>A fresh context over the container, with no identity-map state carried between calls.</summary>
    internal ProbeDbContext NewContext()
        => new(new DbContextOptionsBuilder<ProbeDbContext>().UseNpgsql(container.GetConnectionString()).Options);
}

/// <summary>Binds the fixture to a single collection so the container is started once and shared.</summary>
[CollectionDefinition(nameof(PostgreSqlCollection))]
public sealed class PostgreSqlCollection : ICollectionFixture<PostgreSqlFixture>;
