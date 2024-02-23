CREATE OR ALTER PROCEDURE [dbo].[GetUserDownlineNetworkTreeWithDepth]
    @UserId UNIQUEIDENTIFIER,
    @IncludeSelf BIT = 1,
    @Depth INT = 3
AS
BEGIN
   DECLARE @Table TABLE
    (
        UserId UNIQUEIDENTIFIER,
        ReferredBy UNIQUEIDENTIFIER,
        ParentUserId UNIQUEIDENTIFIER,
        Position INT,
        UserName VARCHAR(50),
        FirstName VARCHAR(50),
        LastName VARCHAR(50),
        DefaultDownlinePlacementPosition INT,
        [Level] INT,
        ChildCount INT,
		ProfilePicture [varbinary](max) NULL,
		CountryId UNIQUEIDENTIFIER
    )

    ;WITH RecursiveCTE AS (
        SELECT
            N1.UserId,
            N1.ReferredBy,
            N1.ParentUserId,
            N1.Position,
            U1.UserName,
            U1.FirstName,
            U1.LastName,
            U1.DefaultDownlinePlacementPosition,
            0 AS Level,
            0 AS ChildCount,
			U1.ProfilePicture,
			U1.CountryId
        FROM
            Networks N1
        INNER JOIN
            Users U1 ON N1.UserId = U1.Id
        WHERE N1.UserId = @UserId

        UNION ALL

        SELECT
            N.UserId,
            N.ReferredBy,
            N.ParentUserId,
            N.Position,
            U.UserName,
            U.FirstName,
            U.LastName,
            U.DefaultDownlinePlacementPosition,
            Level + 1,
            0 AS ChildCount,
			U.ProfilePicture,
			U.CountryId
        FROM
            Networks N
        INNER JOIN
            Users U ON N.UserId = U.Id
        INNER JOIN
            RecursiveCTE ON N.ParentUserId = RecursiveCTE.UserId
        WHERE
            (@Depth IS NULL OR Level < @Depth)
    )
    
    INSERT INTO @Table
    SELECT
        UserId,
        ReferredBy,
        ParentUserId,
        Position,
        UserName,
        FirstName,
        LastName,
        DefaultDownlinePlacementPosition,
        Level,
        (
            SELECT COUNT(*)
            FROM RecursiveCTE Child
            WHERE ParentUserId = RecursiveCTE.UserId
        ) AS ChildCount,
		ProfilePicture,
		CountryId
    FROM
        RecursiveCTE;

    IF @IncludeSelf = 1
    BEGIN
        SELECT t.*,st.[Name] SubscriptionType,su.UserName as Sponsor, c.Name as Country FROM @Table t
				LEFT OUTER JOIN UserPurchasedSubscriptions ups on (t.UserId=ups.UserId)
                Left outer join SubscriptionTypes st on (st.SubscriptionId=ups.SubscriptionId)
					left outer  join Users su on su.Id=t.ReferredBy
				left outer  join Countries c on c.Id=t.CountryId
		ORDER BY t.[Level]

    END
    ELSE
    BEGIN
        SELECT t.*,st.[Name] SubscriptionType,su.UserName as Sponsor, c.Name as Country FROM @Table t 
						LEFT OUTER JOIN UserPurchasedSubscriptions ups on (t.UserId=ups.UserId)
                        Left outer join SubscriptionTypes st on (st.SubscriptionId=ups.SubscriptionId)
											left outer join Users su on su.Id=t.ReferredBy
				left outer join Countries c on c.Id=t.CountryId
		WHERE t.[Level] > 0 ORDER BY t.[Level] 
    END
END