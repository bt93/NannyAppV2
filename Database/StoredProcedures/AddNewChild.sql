USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/9/2022
-- Description:	Creates a new Child
-- =============================================
DROP PROCEDURE IF EXISTS dbo.AddNewChild
GO
CREATE PROCEDURE dbo.AddNewChild
	@FirstName VARCHAR(80),
	@LastName VARCHAR(80),
	@GenderID INT,
	@DateOfBirth DATETIMEOFFSET(7),
	@RatePerHour DECIMAL(9,4),
	@NeedsDiapers BIT,
	@Active BIT,
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	INSERT INTO Child (FirstName, LastName, GenderID, DateOfBirth, RatePerHour, NeedsDiapers, Active)
		VALUES (@FirstName, @LastName, @GenderID, @DateOfBirth, @RatePerHour, @NeedsDiapers, @Active)
	DECLARE @ChildID INT
	SET @ChildID = @@IDENTITY

	INSERT INTO ChildUser (ChildID, UserID)
		VALUES (@ChildID, @UserID)

	COMMIT TRANSACTION
	RETURN 1;
END
GO
