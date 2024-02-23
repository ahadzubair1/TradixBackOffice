public class UserMembership : AuditableEntity, IAggregateRoot
{
    public DefaultIdType UserId { get; set; }
    public DefaultIdType MembershipId { get; set; }
    public DefaultIdType InvestmentId { get; set; }

    public virtual Membership Membership { get; set; }
    public virtual Purchase Investment { get; set; }
    //public virtual UserDirectReferralBonus DirectReferralBonus { get; set; }
}
