using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

public class PostgresTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public DbContextOptions<AppDbContext> DbOptions { get; private set; }

    public PostgresTestFixture()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:15")
            .WithDatabase("dbTest")
            .WithUsername("userTest")
            .WithPassword("postgres")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        DbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        await using var context = new AppDbContext(DbOptions);
        await context.Database.MigrateAsync();
        await DbSeeder.SeedAsync(context);
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}