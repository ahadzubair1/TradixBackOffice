public class NetworkVolume : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType? ReferredBy { get; set; }
    public DefaultIdType MembershipId { get; set; }
    public DefaultIdType MembershipTypeId { get; set; }
    public double Volume { get; set; }
    public NetworkPosition Position { get; set; }
}
