USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/19/2022
-- Description:	Gets the user info by their ID
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUserByID
GO
CREATE PROCEDURE dbo.GetUserByID
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT au.UserID,FirstName, LastName, UserName, 
	EmailAddress, PhoneNumber, IsVerified
	FROM dbo.ApplicationUser au with(nolock)
		WHERE au.UserID = @UserID;
END
GO
