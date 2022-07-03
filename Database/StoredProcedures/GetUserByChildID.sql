USE [NannyDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/3/2022
-- Description:	Gets the user from by the childs id
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUsersByChildID
GO
CREATE PROCEDURE dbo.GetUsersByChildID
	-- Add the parameters for the stored procedure here
	@ChildID INT,
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ChildExists BIT = 0

	IF EXISTS (SELECT UserID FROM ChildUser
				WHERE ChildID = @ChildID
				AND UserID = @UserID)
				
				SET @ChildExists = 1
	
	IF @ChildExists = 1
	SELECT au.* FROM ApplicationUser au
		JOIN ChildUser cu ON cu.UserID = au.UserID
		WHERE cu.ChildID = @ChildID
		AND cu.UserID != @UserID -- Don't need to get current user
	
END
GO
