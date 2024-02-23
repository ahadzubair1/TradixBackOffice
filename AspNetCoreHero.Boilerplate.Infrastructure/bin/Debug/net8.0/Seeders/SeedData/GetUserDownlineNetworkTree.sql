--EXEC [App].[GetUserNetworkTree] '721ed630-8fc0-42b2-80e9-12ee6afb959d'

CREATE OR ALTER PROCEDURE [GetUserDownlineNetworkTree]
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
            0 Level,
            0 AS ChildCount
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
            0 AS ChildCount
        FROM
            Networks N
        INNER JOIN
            Users U ON N.UserId = U.Id
        INNER JOIN
            RecursiveCTE ON N.ParentUserId = RecursiveCTE.UserId
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
            WHERE ParentUserId = RecursiveCTE.UserId --AND Position = RecursiveCTE.Position
        ) AS ChildCount
    FROM
        RecursiveCTE;

	IF @IncludeSelf = 1
	BEGIN
		SELECT * FROM @Table ORDER BY [Level]
	END
	ELSE
	BEGIN
		SELECT * FROM @Table WHERE [Level] > 0 ORDER BY [Level] 
	END
	--IF EXISTS (SELECT * FROM @Table WHERE Position = @Position AND ChildCount = 0)
	--BEGIN
	--	--PRINT 'Child Found'
	--	SELECT * FROM @Table WHERE Position = @Position AND ChildCount = 0
	--END
	--ELSE
	--BEGIN
	--	--PRINT 'Child Not Found'
	--	SELECT * FROM @Table --WHERE UserId = @UserId
	--END
END;