USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/3/2022
-- Description:	Gets the user that are connected to other users by their children
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUserConnectedByChild
GO
CREATE PROCEDURE dbo.GetUserConnectedByChild
	@UserID INT,
	@RoleID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM ApplicationUser au
		WHERE au.RoleID = @RoleID AND
		au.UserID IN 
			(SELECT UserID FROM Child c
				JOIN ChildUser cu ON cu.ChildID = c.ChildID
				WHERE cu.UserID != @UserID)
END
GO
