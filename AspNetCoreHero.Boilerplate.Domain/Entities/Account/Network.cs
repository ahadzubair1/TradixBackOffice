[Table("Networks")]
public class Network : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType ReferredBy { get; set; }
    public DefaultIdType? ParentUserId { get; set; }
    public NetworkPosition? Position { get; set; }
    //public DefaultIdType? LeftUserId { get; set; }
    //public DefaultIdType? RightUserId { get; set; }

    public List<VolumeDetails>? NetworkVolumes { get; set; }
}
