using Application.DTO;

namespace Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccountDto?> GetByIdAsync(Guid id);

    Task<List<BankAccountDto>> GetAllAsync();

    Task<Guid?> AddAsync(BankAccountAddDto bankAccount);
}