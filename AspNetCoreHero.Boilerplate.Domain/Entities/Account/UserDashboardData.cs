namespace VM.WebApi.Domain.App;

public class UserDashboardData : BaseEntity
{
    public string ReferredBy { get; set; }
    public int DiectBonus { get; set; }
    public int CommunityBonus { get; set; }
    public string Subscription { get; set; }
    public string DirectReferrals { get; set; }
    public int TotalLeftNetworkUsers { get; set; }
    public int TotalRightNetworkUser { get; set; }
    public int CommunityBonusWallet { get; set; }
    public string CurrentRank { get; set; }
    public int RankVolumeleft { get; set; }
    public int RankVolumeRight { get; set; }
    public int NetworkVolumeLeft { get; set; }
    public int NetworkVolumeRight { get; set; }
    public string ReferralLinkLeft { get; set; }
    public string ReferralLinkRight { get; set; }
    public string ReferralLinkAlternate { get; set; }
}