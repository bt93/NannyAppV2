USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/4/2022
-- Description:	Updates the User to IsVerified
-- =============================================
DROP PROCEDURE IF EXISTS dbo.VerifyUser
GO
CREATE PROCEDURE dbo.VerifyUser
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	If EXISTS (SELECT UserID FROM ApplicationUser 
				WHERE IsVerified = 0
				AND UserID = @UserID)
		UPDATE ApplicationUser
			SET IsVerified = 1
			WHERE UserID = @UserID;
	RETURN @@RowCount
END
GO
