using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _dbContext;

    public BankAccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        return await _dbContext.BankAccounts
            .Include(b => b.Transactions)
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<BankAccount>> GetAllAsync()
    {
        return await _dbContext.BankAccounts.ToListAsync();
    }

    public async Task<Guid> AddBankAccountAsync(BankAccount account)
    {
        await _dbContext.BankAccounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();

        return account.Id;
    }
}