USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/17/2022
-- Description:	Updates a users password
-- =============================================
DROP PROCEDURE IF EXISTS dbo.UpdateUserPassword
GO
CREATE PROCEDURE dbo.UpdateUserPassword 
	@UserID INT,
	@Password VARCHAR(250),
	@Salt VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE ApplicationUser
	SET Password = @Password,
	Salt = @Salt
	WHERE UserID = @UserID

	RETURN @@ROWCOUNT

END
GO
