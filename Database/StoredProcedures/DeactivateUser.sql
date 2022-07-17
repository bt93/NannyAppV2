USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/17/2022
-- Description: Decativates a user
-- =============================================
DROP PROCEDURE IF EXISTS dbo.DeactivateUser
GO
CREATE PROCEDURE dbo.DeactivateUser
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE ApplicationUser
	SET IsActive = 0
	WHERE UserID = @UserID

	RETURN @@ROWCOUNT
END
GO
