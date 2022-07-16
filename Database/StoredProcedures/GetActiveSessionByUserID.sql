USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/16/2022
-- Description:	Gets a users active sessions
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetActiveSessionByUserID
GO
CREATE PROCEDURE dbo.GetActiveSessionByUserID 
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT s.* FROM Session s WITH(NOLOCK)
		JOIN SessionUser su ON su.SessionID = s.SessionID
		WHERE su.UserID = 2
		AND (s.PickUp IS NULL OR s.PickUp > GETDATE())
END
GO
