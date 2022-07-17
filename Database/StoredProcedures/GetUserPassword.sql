USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/17/2022
-- Description:	Gets a users password
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUserPassword
GO
CREATE PROCEDURE dbo.GetUserPassword 
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Password, Salt FROM ApplicationUser WITH(NOLOCK)
		WHERE UserID = @UserID
END
GO
