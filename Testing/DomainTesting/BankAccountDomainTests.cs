using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Testing.DomainTesting;

public class BankAccountDomainTests(PostgresTestFixture fixture) : IntegrationTest(fixture)
{
    [Fact]
    public async Task GetSeededAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        // Act
        var account = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
    }

    [Fact]
    public async Task AddMovementsToBankAccount_Should_ReturnOk()
    {
        // Arrange
        Guid id = new("11111111-1111-1111-1111-111111111111");

        var movement1 = new BankTransaction((decimal)1.6, id);
        var movement2 = new BankTransaction((decimal)4.7, id);

        // Act
        await _context.Transactions.AddRangeAsync(movement1, movement2);
        await _context.SaveChangesAsync();

        var account = await _context.BankAccounts
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id == id);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Test Account 1", account.Name);
        Assert.Equal(2, account.Transactions.Count);

        Assert.Contains(account.Transactions, x => x.Id == movement1.Id);
        Assert.Contains(account.Transactions, x => x.Id == movement2.Id);
        Assert.Equal((decimal)6.3, account.Transactions.Sum(x => x.Amount));
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

    [Fact]
    public void Withdraw_Should_Decrease_Balance_When_Amount_Is_Less_Than_Balance()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Test Account", 100m);

        // Act
        account.Withdraw(40m);

        // Assert
        Assert.Equal(60m, account.Balance);
        Assert.Single(account.Transactions);
        Assert.Equal(-40m, account.Transactions.First().Amount);
    }

    [Fact]
    public void Withdraw_Should_Throw_Exception_When_Amount_Is_Negative()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Test Account", 100m);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => account.Withdraw(-10m));
        Assert.Equal("Amount must be greater than 0 (Parameter 'amount')", ex.Message);
    }
}
