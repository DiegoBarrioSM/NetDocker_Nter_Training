namespace Domain.Entities;

public class BankTransaction
{
    private BankTransaction() { }

    public BankTransaction(decimal amount, Guid accountId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        AccountId = accountId;
    }

    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }

    public Guid AccountId { get; private set; }
    public BankAccount BankAccount { get; private set; } = null!;
}
