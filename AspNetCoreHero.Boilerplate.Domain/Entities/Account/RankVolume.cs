public class RankVolume : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType? ReferredBy { get; set; }
    public DefaultIdType MembershipId { get; set; }
    public DefaultIdType MembershipTypeId { get; set; }
    public double Volume { get; set; }
    public NetworkPosition Position { get; set; }
    public virtual Membership Membership { get; set; }
}
