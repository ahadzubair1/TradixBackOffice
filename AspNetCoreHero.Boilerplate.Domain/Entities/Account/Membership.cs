public class Membership : AuditableEntity, IAggregateRoot
{

    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string? Code { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public DefaultIdType? MembershipTypeId { get; set; }
    public virtual MembershipType? MembershipType { get; set; }
    public double Cap { get; set; }
    public double Price { get; set; } = 0;
    public bool EnableRankVolume { get; set; }
    public bool EnableNetworkVolume { get; set; }
    public bool EnableNetworkBonus { get; set; }
    public bool EnableVBonus { get; set; }
    public bool EnableDirectReferralBonus { get; set; }
    public bool EnableVMastery { get; set; }
    public bool EnablePad22 { get; set; }
    public bool EnableVWards { get; set; }
    public bool IsMembershipRequired { get; set; }
    public bool EnableStaking { get; set; }
    public bool EnableMetaverse { get; set; }
    [MaxLength(500)]
    public string? ImageUri { get; set; }
    public ExpiryIntervalTypes ExpiryIntervalType { get; set; } = ExpiryIntervalTypes.Weekly;
    public int ExpiryInterval { get; set; } = 60;
    public double LoyalityPercentage { get; set; } = 2.2;
    public double DailyReturnPercentage { get; set; }
    public double DirectReferralBonusPrecentage { get; set; } = 10;
    public int RankVolume { get; set; }
    public int NetworkVolume { get; set; }
}