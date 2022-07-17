USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/9/2022
-- Description:	Gets a Session by the id
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetSessionByID
GO
CREATE PROCEDURE dbo.GetSessionByID
	@SessionID INT,
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS ((SELECT su.UserID FROM SessionUser su WITH(NOLOCK)
				JOIN ApplicationUser au ON au.UserID = su.UserID
				JOIN UserRole r ON r.UserID = au.UserID
				WHERE su.SessionID = @SessionID
				AND au.UserID = @UserID))
		SELECT s.* FROM Session s WITH(NOLOCK)
			WHERE SessionID = @SessionID;
	ELSE IF EXISTS (SELECT UserID FROM UserRole WITH(NOLOCK) WHERE UserID = @UserID AND RoleID = 3)
		SELECT s.* FROM Session s WITH(NOLOCK)
				WHERE SessionID = @SessionID;

END
GO
