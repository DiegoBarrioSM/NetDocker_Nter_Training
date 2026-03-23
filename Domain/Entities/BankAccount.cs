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

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than 0", nameof(amount));

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds");

        var transaction = new BankTransaction(-amount, Id);
        Transactions.Add(transaction);

        Balance -= amount;
    }
}
