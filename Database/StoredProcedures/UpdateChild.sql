USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/17/2022
-- Description:	Updates a childs info
-- =============================================
DROP PROCEDURE IF EXISTS dbo.UpdateChild
GO
CREATE PROCEDURE dbo.UpdateChild
	@ChildID INT,
	@FirstName VARCHAR(80),
	@LastName VARCHAR(80),
	@GenderID INT,
	@DateOfBirth DATETIMEOFFSET(7),
	@RatePerHour DECIMAL(9,4),
	@NeedsDiapers BIT,
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF EXISTS (SELECT au.UserID FROM ApplicationUser au
				JOIN ChildUser cu ON cu.UserID = au.UserID
				WHERE au.UserID = @UserID AND cu.ChildID = @ChildID)

		UPDATE Child
		SET FirstName = @FirstName,
		LastName = @LastName,
		GenderID = @GenderID,
		DateOfBirth = @DateOfBirth,
		RatePerHour = @RatePerHour,
		NeedsDiapers = @NeedsDiapers
		WHERE ChildID = @ChildID
	
	ELSE IF EXISTS (SELECT ur.UserID FROM UserRole ur
					WHERE ur.UserID = @UserID AND ur.RoleID = 3)
							UPDATE Child
		SET FirstName = @FirstName,
		LastName = @LastName,
		GenderID = @GenderID,
		DateOfBirth = @DateOfBirth,
		RatePerHour = @RatePerHour,
		NeedsDiapers = @NeedsDiapers
		WHERE ChildID = @ChildID

	RETURN @@ROWCOUNT
END
GO
