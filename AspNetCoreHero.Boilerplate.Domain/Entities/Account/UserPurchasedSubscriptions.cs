public class UserPurchasedSubscriptions : AuditableEntity, IAggregateRoot
{
    public DefaultIdType Id { get;set; }
    public DefaultIdType UserId { get; set; }
    public string? UserName { get; set; }
    public int SubscriptionId { get; set; }
    public float Amount { get; set; }
    public long PurchasedAt { get; set; }
    public DateTime SubscriptionPurchaseDate { get; set; }
    public DateTime SubscriptionStartDate { get; set; }
    public long SubscriptionStartsAt { get; set; }
    public long SubscriptionExpiry { get; set; }
    public long SubscriptionEndsAt { get; set; }
    public DateTime SubscriptionEndDate { get; set; }
    public DateTime SubscriptionExpiryDate { get; set; }
    public string SubscriptionType { get; set; }
    public bool IsActive { get; set; }

}