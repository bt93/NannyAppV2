USE [NannyDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/4/2022
-- Description:	Updates a users first name, last name and  phone number
-- =============================================
DROP PROCEDURE IF EXISTS dbo.UpdateUser
GO
CREATE PROCEDURE dbo.UpdateUser
	@UserID INT,
	@FirstName VARCHAR(80),
	@LastName VARCHAR(80),
	@PhoneNumber VARCHAR(20)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE ApplicationUser 
	SET FirstName = @FirstName,
	LastName = @LastName,
	PhoneNumber = @PhoneNumber
	WHERE UserID = @UserID
	RETURN @@RowCount
END
GO
