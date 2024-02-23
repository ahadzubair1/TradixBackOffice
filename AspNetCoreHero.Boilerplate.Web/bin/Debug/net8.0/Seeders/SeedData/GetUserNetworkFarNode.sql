CREATE OR ALTER PROCEDURE [GetUserNetworkFarNode]
    @UserId UNIQUEIDENTIFIER,
	@Position INT
AS
BEGIN

--DECLARE
--	@UserId UNIQUEIDENTIFIER = '1B58893F-ED63-4151-BDF8-9A142E2E8241',
--	@Position INT = 1
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
        WHERE N1.UserId = @UserId --AND N1.Position = @Position

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
            RecursiveCTE ON N.ParentUserId = RecursiveCTE.UserId AND N.Position = @Position
    )
    

    -- Final SELECT from the CTE
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

	--SELECT * FROM @Table
	-- Update the ChildCount in RecursiveCTE to represent the total count of children recursively
    --UPDATE @Table
    --SET ChildCount = (
    --    SELECT COUNT(*)
    --    FROM @Table Child
    --    WHERE RecursiveCTE.UserId = Child.ParentUserId
    --);
	IF EXISTS (SELECT * FROM @Table WHERE Position = @Position AND ChildCount = 0)
	BEGIN
		--PRINT 'Child Found'
		SELECT * FROM @Table WHERE Position = @Position AND ChildCount = 0
	END
	ELSE
	BEGIN
		--PRINT 'Child Not Found'
		SELECT * FROM @Table --WHERE UserId = @UserId
	END
END;