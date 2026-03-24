namespace Application.DTO;

public class BankAccountDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Balance { get; set; } = 0;

    public List<BankTransactionDto> Transactions { get; set; } = [];
}