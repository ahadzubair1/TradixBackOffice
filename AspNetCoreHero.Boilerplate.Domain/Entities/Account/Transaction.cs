public enum TransactionType
{
    Withdrawal = 1,
    Deposit = 2,
    Transfer = 3,
    Fee = 4,
    Purchase = 5
}
public enum TransactionCategory
{
    Unknown = 0,
    Debit = 1,
    Credit = 2
}

public enum TransactionStatus
{
    Unknown = 0,
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Reversed = 4,
}

public static class EnumExtension
{
    public static TransactionCategory MapTransactionCategory(this TransactionType status)
    {
        switch (status)
        {
            case TransactionType.Purchase:
            case TransactionType.Transfer:
            case TransactionType.Withdrawal:
            case TransactionType.Fee:
                return TransactionCategory.Credit;
            case TransactionType.Deposit:
                return TransactionCategory.Debit;
            default:
                return TransactionCategory.Unknown;
        }
    }
}

public class Transaction : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public string? ReferenceId { get; set; }
    public string? Source { get; set; }
    public string? Destination { get; set; }
    public double Amount { get; set; }
    public WalletProvider WalletProvider { get; set; }
    public string? WalletAddress { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionCategory TransactionCategory { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
}
