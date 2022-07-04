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
CREATE PROCEDURE dbo.VerifyUser
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE ApplicationUser
		SET IsVerified = 1
		WHERE UserID = @UserID;
END
GO
