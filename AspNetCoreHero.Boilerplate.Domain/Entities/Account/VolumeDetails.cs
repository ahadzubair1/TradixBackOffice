public class VolumeDetails : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType MembershipTypeId { get; set; }
    public MembershipType MembershipType { get; set; }
    public double NetworkVolumeLeft { get; set; }
    public double NetworkVolumeRight { get; set; }
    public double RankVolumeLeft { get; set; }
    public double RankVolumeRight { get; set; }
    public DefaultIdType? HighestMembershipId { get; set; }
    public UserMembership? HighestMembership { get; set; }
}
