USE [NannyDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/4/2022
-- Description:	Gets a single child by thier ID, also requires UserID
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetChildByID
GO
CREATE PROCEDURE dbo.GetChildByID
	@ChildID INT,
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS (
		SELECT UserID FROM 
		ApplicationUser
		WHERE UserID = @UserID
		AND RoleID = 3)
	
		SELECT c.* FROM Child c WITH(NOLOCK)
		WHERE c.ChildID = @ChildID

	ELSE

		SELECT c.* FROM Child c WITH(NOLOCK)
		JOIN ChildUser cu ON cu.ChildID = c.ChildID
		WHERE c.ChildID = @ChildID
		AND cu.UserID = @UserID
END
GO
