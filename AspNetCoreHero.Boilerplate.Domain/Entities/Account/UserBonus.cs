public class UserBonus : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType MembershipTypeId { get; set; }
    public double Amount { get; set; }
    public NetworkPosition? NetworkPosition { get; set; }
}
