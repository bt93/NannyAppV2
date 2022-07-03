USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/19/2022
-- Description:	Gets the user password by UserName or Email
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUserForLogin
GO

CREATE PROCEDURE dbo.GetUserForLogin
	@UserInput VARCHAR(120)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT UserID, EmailAddress, UserName, Password, Salt, RoleID FROM ApplicationUser with(nolock)
		WHERE (UserName = @UserInput OR EmailAddress = @UserInput);
END
GO
