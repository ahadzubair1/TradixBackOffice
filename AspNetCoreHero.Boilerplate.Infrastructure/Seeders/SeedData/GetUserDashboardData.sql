CREATE PROCEDURE [dbo].[GetUserDashboardData] 
 @UserId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @Table TABLE
    (
        ReferredBy NVARCHAR(100),
        DiectBonus INT,
		CommunityBonus INT,
		Subscription NVARCHAR(100),
        DirectReferrals INT,
        TotalLeftNetworkUsers INT,
		TotalRightNetworkUser INT,
		CommunityBonusWallet INT,
		CurrentRank NVARCHAR(100),
		RankVolumeleft int,
		RankVolumeRight INT,
		NetworkVolumeLeft INT,
		NetworkVolumeRight INT,
		ReferralLinkLeft NVARCHAR(300),
		ReferralLinkRight NVARCHAR(300),
		ReferralLinkAlternate NVARCHAR(300)
    )

        DECLARE @ReferredBy NVARCHAR(100)
        DECLARE @DirectBonus INT
		DECLARE @CommunityBonus INT
		DECLARE @Subscription NVARCHAR(100)
        DECLARE @DirectReferrals INT
        DECLARE @TotalLeftNetworkUsers INT
		DECLARE @TotalRightNetworkUser INT
		DECLARE @CommunityBonusWallet INT
		DECLARE @CurrentRank NVARCHAR(100)
		DECLARE @RankVolumeleft int
		DECLARE @RankVolumeRight INT
		DECLARE @NetworkVolumeLeft INT
		DECLARE @NetworkVolumeRight INT
		DECLARE @ReferralLinkLeft NVARCHAR(300)
		DECLARE @ReferralLinkRight NVARCHAR(300)
		DECLARE @ReferralLinkAlternate NVARCHAR(300)

		Set @ReferredBy = (Select FirstName+' '+LastName from Users where Id in (Select ReferredBy from Users Where Id = @UserId and IsActive=1) )
		Set @DirectBonus = 10
		Set @Subscription = (Select SubscriptionTypes.Name 
								from UserPurchasedSubscriptions inner join SubscriptionTypes
								On UserPurchasedSubscriptions.SubscriptionId = SubscriptionTypes.SubscriptionId
								Where UserPurchasedSubscriptions.UserId = @UserId and UserPurchasedSubscriptions.Status = 1)
		Set @DirectReferrals = (Select Count(UserId) from DirectReferrals where ReferredBy = @UserId and Status=1)
		Set @CommunityBonusWallet =10
		Set @TotalLeftNetworkUsers = (Select Count(*) from Networks where ReferredBy = @UserId and Position =1 and Status=1)
		Set @TotalRightNetworkUser = (Select Count(*) from Networks where ReferredBy = @UserId and Position =2 and Status=1)

		Select @CurrentRank = (Select Ranks.Name from UserRanks inner join Ranks 
								on UserRanks.RankId = Ranks.Id   where UserId = @UserId and UserRanks.Status = 1)

		Set @RankVolumeleft = (Select Count(UserId) from RankVolumes where ReferredBy = @UserId and Position =1 and Status=1)
		Set @RankVolumeRight = (Select Count(UserId) from RankVolumes where ReferredBy = @UserId and Position =2)

		Set @NetworkVolumeLeft = (Select Count(UserId) from NetworkVolumes where ReferredBy = @UserId and Position =1 and Status=1)
		Set @NetworkVolumeRight = (Select Count(UserId) from NetworkVolumes where ReferredBy = @UserId and Position =2 and Status=1)

		Set @ReferralLinkLeft = (Select Code from ReferralCodes where UserId = @UserId and NetworkPosition = 1 and Status = 1)
		Set @ReferralLinkRight = (Select Code from ReferralCodes where UserId = @UserId and NetworkPosition = 2 and Status = 1)
		Set @ReferralLinkAlternate = (Select Code from ReferralCodes where UserId = @UserId and NetworkPosition = 3 and Status = 1)

		INSERT INTO @Table(ReferredBy,
							DiectBonus,
							CommunityBonus,
							Subscription,
							DirectReferrals,
							TotalLeftNetworkUsers ,
							TotalRightNetworkUser ,
							CommunityBonusWallet ,
							CurrentRank,
							RankVolumeleft,
							RankVolumeRight,
							NetworkVolumeLeft,
							NetworkVolumeRight ,
							ReferralLinkLeft,
							ReferralLinkRight ,
							ReferralLinkAlternate )
							VALUES(@ReferredBy, ISNULL(@DirectBonus,0), ISNULL(@CommunityBonus, 0), @Subscription, @DirectReferrals, @TotalLeftNetworkUsers, @TotalRightNetworkUser,
									ISNULL(@CommunityBonusWallet,0), @CurrentRank, @RankVolumeleft, @RankVolumeRight, @NetworkVolumeLeft, @NetworkVolumeRight, 
									@ReferralLinkLeft, @ReferralLinkRight, @ReferralLinkAlternate)

		SELECT * from @Table
END
