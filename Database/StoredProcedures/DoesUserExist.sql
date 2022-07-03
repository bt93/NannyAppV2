USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/19/2022
-- Description:	Sends 1 if username or email already exists.
-- =============================================
DROP PROCEDURE IF EXISTS dbo.DoesUserExist
GO
CREATE PROCEDURE dbo.DoesUserExist 
	@UserName VARCHAR(80),
	@EmailAddress VARCHAR(120)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @userNameExists bit = 0
	declare @emailAddressExists bit = 0

    IF EXISTS(
	SELECT UserID
		FROM dbo.ApplicationUser with(nolock)
		WHERE UserName = @UserName)
	
		SET @userNameExists = 1

	IF EXISTS(
	SELECT UserID
		FROM dbo.ApplicationUser with(nolock)
		WHERE EmailAddress = @EmailAddress)
	
		SET @emailAddressExists = 1
	
	SELECT @userNameExists AS UserNameExists;
	SELECT @emailAddressExists AS EmailAddressExists;
	
END
GO
