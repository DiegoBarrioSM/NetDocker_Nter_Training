using Infrastructure.Data;
using Xunit;

public abstract class IntegrationTest : IClassFixture<PostgresTestFixture>
{
    protected readonly AppDbContext _context;

    protected IntegrationTest(PostgresTestFixture fixture)
    {
        _context = new AppDbContext(fixture.DbOptions);
    }
}