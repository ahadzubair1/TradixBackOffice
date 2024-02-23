public class Deposit : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public double Amount { get; set; }
    public string? TransactionId { get; set; }
    public DateTime TransactionTime { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public string? Details { get; set; }
}
