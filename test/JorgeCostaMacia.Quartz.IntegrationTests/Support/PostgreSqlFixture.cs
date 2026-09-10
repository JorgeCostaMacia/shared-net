using Npgsql;
using Testcontainers.PostgreSql;

namespace JorgeCostaMacia.Quartz.IntegrationTests.Support;

/// <summary>
/// A real Postgres for the store tests, with the schema the prefix points at created up front:
/// <c>ProvisionSchema()</c> creates Quartz's tables but not the schema holding them.
/// </summary>
public sealed class PostgreSqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:16").Build();

    /// <summary>The schema the store's table prefix points at, as <c>bus</c> does in the workers.</summary>
    public const string Schema = "bus";

    /// <summary>The connection string of the running container.</summary>
    public string ConnectionString => _container.GetConnectionString();

    /// <inheritdoc />
    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync(TestContext.Current.CancellationToken);

        await using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync(TestContext.Current.CancellationToken);
        await using NpgsqlCommand command = new NpgsqlCommand($"CREATE SCHEMA IF NOT EXISTS {Schema};", connection);
        await command.ExecuteNonQueryAsync(TestContext.Current.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync() => await _container.DisposeAsync();
}

/// <summary>The collection sharing one container across the store tests.</summary>
[CollectionDefinition(nameof(PostgreSqlCollection))]
public sealed class PostgreSqlCollection : ICollectionFixture<PostgreSqlFixture>;
