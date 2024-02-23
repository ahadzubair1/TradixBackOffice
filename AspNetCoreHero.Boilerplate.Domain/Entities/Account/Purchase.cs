public class Purchase : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public double Amount { get; set; }
    public DefaultIdType? MembershipId { get; set; }
    public DefaultIdType? SubscriptionId { get; set; }
    public virtual Membership? Membership { get; set; }
    public DefaultIdType? WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public ExpiryIntervalTypes ExpiryIntervalType { get; set; } = ExpiryIntervalTypes.Weekly;
    public int ExpiryInterval { get; set; } = 60;
    public int ElapsedInterval { get; set; } = 0;
    public double Cap { get; set; } = 0;
    public bool IsExpired { get; set; } = false;

    public DefaultIdType TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; }

}
