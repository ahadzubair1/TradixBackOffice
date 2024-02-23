CREATE OR ALTER VIEW UsersNetworkTree
AS
WITH RecursiveCTE AS (
    SELECT
        N1.UserId,
        N1.ReferredBy,
        N1.ParentUserId,
		N1.Position,
        U1.UserName,
        U1.FirstName,
        U1.LastName,
        U1.DefaultDownlinePlacementPosition,
		0 Level, -- Incrementing the level for each recursion
        0 AS ChildCount -- Initialize ChildCount to 0
    FROM
        Networks N1
    INNER JOIN
        Users U1 ON N1.UserId = U1.Id
    WHERE N1.ParentUserId IS NULL -- Assuming NULL indicates the root level

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
        Level + 1, -- Incrementing the level for each recursion
        0 AS ChildCount -- Initialize ChildCount to 0 for each child node
    FROM
        Networks N
    INNER JOIN
        Users U ON N.UserId = U.Id
    INNER JOIN
        RecursiveCTE ON N.ParentUserId = RecursiveCTE.UserId
)
-- Final SELECT from the CTE
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
    ) AS ChildCount
FROM
    RecursiveCTE;

