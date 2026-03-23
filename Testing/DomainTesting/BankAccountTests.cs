using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Testing.DomainTesting;

public class BankAccountTests : IntegrationTest
{
    public BankAccountTests(PostgresTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetSeededAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        // Act
        var account = await _context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
    }

    [Fact]
    public async Task AddMovementsToBankAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        var transaction1 = new BankTransaction(1.6m, id);
        var transaction2 = new BankTransaction(4.7m, id);

        // Act
        await _context.Transactions.AddRangeAsync(transaction1, transaction2);
        await _context.SaveChangesAsync();

        var account = await _context.BankAccounts
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
        Assert.Equal(2, account.Transactions.Count);

        Assert.Contains(account.Transactions, t => t.Id == transaction1.Id);
        Assert.Contains(account.Transactions, t => t.Id == transaction2.Id);
        Assert.Equal(6.3m, account.Transactions.Sum(x => x.Amount));
    }

    [Fact]
    public async Task AddBankAccount_Should_Ok()
    {
        // Arrange
        var ba = new BankAccount(Guid.NewGuid(), "name1", 34);

        // Act
        _context.BankAccounts.Add(ba);
        await _context.SaveChangesAsync();

        var account = await _context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id == ba.Id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(ba.Id, account.Id);
        Assert.Equal("name1", account.Name);
        Assert.Equal(34, account.Balance);
    }
}