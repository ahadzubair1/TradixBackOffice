public class Withdrawal : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public double Amount { get; set; }
}
