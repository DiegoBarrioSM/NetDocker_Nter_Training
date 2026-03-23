using Domain.Entities;

namespace Domain.IRepositories;

public interface IBankAccountRepository
{
    Task<BankAccount> GetByIdAsync(Guid id);

    Task<List<BankAccount>> GetAllAsync();

    Task<Guid> AddBankAccountAsync(BankAccount account);

}
