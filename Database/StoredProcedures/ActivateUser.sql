USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/17/2022
-- Description: Activates a user
-- =============================================
DROP PROCEDURE IF EXISTS dbo.ActivateUser
GO
CREATE PROCEDURE dbo.ActivateUser 
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE ApplicationUser
	SET IsActive = 1
	WHERE UserID = @UserID

	RETURN @@ROWCOUNT
END
GO
