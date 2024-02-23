public class Wallet : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType WalletTypeId { get; set; }
    public virtual WalletType WalletType { get; set; }
    public double Balance { get; set; }
}