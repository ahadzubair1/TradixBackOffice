CREATE OR ALTER PROCEDURE [GenerateRankVolume]
(
	@UserId UNIQUEIDENTIFIER, --= '2981470d-02d7-4b04-97a2-dbacbf7e0dc2',
	@Position INT-- = 1
)
AS
SET NOCOUNT ON
DECLARE @jj INT = 1
DECLARE @count_jj INT = 0

DECLARE @UId UNIQUEIDENTIFIER
DECLARE @MembershipTypeId UNIQUEIDENTIFIER
DECLARE @ReferredBy UNIQUEIDENTIFIER
DECLARE @Volume DECIMAL(18,5)

DECLARE @Tree TABLE
(
	SNo INT IDENTITY(1,1),
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

SELECT @ReferredBy = ReferredBy, @MembershipTypeId = MembershipTypeId, @Volume = SUM(Volume) FROM RankVolumes 
WHERE UserId = @UserId AND Position = @Position AND Status = 1
GROUP BY UserId, ReferredBy,MembershipTypeId,Position


IF @MembershipTypeId IS NULL
	RETURN

INSERT INTO @Tree
EXEC GetUserUplineReferralTree @UserId
DELETE FROM @Tree WHERE [Level] = 0

SELECT @count_jj = COUNT(*) FROM @Tree

WHILE @jj <= @count_jj
BEGIN
	SELECT @UId = UserId FROM @Tree WHERE Sno = @jj
	PRINT @UId

	IF NOT EXISTS(SELECT 1 FROM VolumeDetails WHERE UserId = @UId)
	BEGIN
		PRINT 'NOT Exists'

		INSERT INTO [VolumeDetails]
		        ([Id]
		        ,[UserId]
		        ,[MembershipTypeId]
		        ,[NetworkVolumeLeft]
		        ,[NetworkVolumeRight]
		        ,[RankVolumeLeft]
		        ,[RankVolumeRight]
		        ,[HighestMembershipId]
		        ,[NetworkId]
		        ,[CreatedBy]
		        ,[CreatedOn]
		        ,[LastModifiedBy]
		        ,[LastModifiedOn]
		        ,[DeletedOn]
		        ,[DeletedBy]
		        ,[Status])
		VALUES
		        (NEWID()
		        ,@UId
		        ,@MembershipTypeId
		        ,0
		        ,0
		        ,0
		        ,0
		        ,NULL
		        ,NULL
		        ,NEWID()
		        ,GETDATE()
		        ,NEWID()
		        ,GETDATE()
		        ,NULL
		        ,NULL
		        ,1)
			
	END



	IF(@Position = 2)
	BEGIN
		UPDATE [VolumeDetails] SET RankVolumeRight = ISNULL(RankVolumeRight, 0) + @Volume WHERE UserId = @UId
		UPDATE RankVolumes SET Status = 2 WHERE UserId = @UserId AND Status = 1
	END
	ELSE IF(@Position = 1)
	BEGIN
		UPDATE [VolumeDetails] SET RankVolumeLeft = ISNULL(RankVolumeLeft, 0) + @Volume WHERE UserId = @UId
		UPDATE RankVolumes SET Status = 2 WHERE UserId = @UserId AND Status = 1
	END
	PRINT @jj
	SET @jj = @jj + 1
END