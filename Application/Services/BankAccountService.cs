using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _repository;

    public BankAccountService(IBankAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<BankAccountDto?> GetByIdAsync(Guid id)
    {
        var account = await _repository.GetByIdAsync(id);

        if (account == null)
            return null;

        return new BankAccountDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance,
            Transactions = [.. account.Transactions.Select(x => new BankTransactionDto
            {
                Id = x.Id,
                Amount = x.Amount
            })],
        };
    }

    public async Task<List<BankAccountDto>> GetAllAsync()
    {
        var accounts = await _repository.GetAllAsync();

        return [.. accounts.Select(account => new BankAccountDto
        {
            Id = account.Id,
            Name = account.Name,
            Balance = account.Balance
        })];
    }

    public async Task<Guid?> AddAsync(BankAccountAddDto dto)
    {
        var newBankAccount = new BankAccount(dto.Name, dto.Balance);

        var id = await _repository.AddBankAccountAsync(newBankAccount);

        return id;
    }
}