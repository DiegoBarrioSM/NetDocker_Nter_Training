using Domain.Entities;
using Infrastructure.Repositories;

namespace Testing.InfrastructureTesting;

public class BankAccountInfrastructureTests : IntegrationTest
{
    private readonly BankAccountRepository _repository;

    public BankAccountInfrastructureTests(PostgresTestFixture fixture) : base(fixture)
    {
        _repository = new BankAccountRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsyncByDefaultId_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        // Act
        var account = await _repository.GetByIdAsync(id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account!.Name);
    }

    [Fact]
    public async Task InsertAndGetAccount_Should_ReturnOk()
    {
        // Arrange
        BankAccount ba = new("name1", 57);

        // Act
        var newId = await _repository.AddBankAccountAsync(ba);
        var account = await _repository.GetByIdAsync(newId);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("name1", account!.Name);
    }
}