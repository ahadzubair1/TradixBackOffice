CREATE OR ALTER PROCEDURE [GetUserUplineReferralTree]
    @UserId UNIQUEIDENTIFIER,
	@IncludeSelf BIT = 1
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
        ChildCount INT
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
        0 AS ChildCount
    FROM
        Networks N1
    INNER JOIN
        Users U1 ON N1.UserId = U1.Id
    WHERE N1.UserId = @UserId

    UNION ALL

    SELECT
        N2.UserId,
        N2.ReferredBy,
        N2.ParentUserId,
        N2.Position,
        U2.UserName,
        U2.FirstName,
        U2.LastName,
        U2.DefaultDownlinePlacementPosition,
        Level + 1 AS Level,
        0 AS ChildCount
    FROM
        Networks N2
    INNER JOIN
        Users U2 ON N2.UserId = U2.Id
    INNER JOIN
        RecursiveCTE ON N2.UserId = RecursiveCTE.ReferredBy
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
            WHERE ReferredBy = RecursiveCTE.UserId --AND Position = RecursiveCTE.Position
        )
    FROM
        RecursiveCTE --ORDER BY Level DESC
    
	
	IF @IncludeSelf = 1
	BEGIN
		SELECT * FROM @Table ORDER BY [Level] DESC
	END
	ELSE
	BEGIN
		SELECT * FROM @Table WHERE [Level] > 0 ORDER BY [Level] DESC
	END
END;