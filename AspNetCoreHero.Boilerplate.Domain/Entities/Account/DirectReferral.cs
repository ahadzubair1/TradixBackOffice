
public class DirectReferral : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType ReferredBy { get; set; }
    public DefaultIdType MembershipTypeId { get; set; }
    public virtual MembershipType MembershipType { get; set; }
    public DefaultIdType MembershipId { get; set; }
    public virtual Membership Membership { get; set; }
    public double MembershipPrice { get; set; }
    public double Percentage { get; set; }
    public double Amount { get; set; }
    public NetworkPosition? Position { get; set; }
    public bool IsRepurchased { get; set; }
    public bool IsEligible { get; set; }
    //public bool IsActive { get; set; }
}
