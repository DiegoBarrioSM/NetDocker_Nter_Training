namespace Domain.Entities;

public class BankAccount
{
    public BankAccount(Guid id, string name, decimal balance)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Balance = balance;
    }

    public BankAccount(string name, decimal balance)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Balance = balance;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public decimal Balance { get; private set; }

    public ICollection<BankTransaction> Transactions { get; private set; } = [];
}
