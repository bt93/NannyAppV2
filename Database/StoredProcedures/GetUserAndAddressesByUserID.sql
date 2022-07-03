Use NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/19/2022
-- Description:	Gets a User and any addresses
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetUserAndAddressesByUserID
GO
CREATE PROCEDURE dbo.GetUserAndAddressesByUserID
	-- Add the parameters for the stored procedure here
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT au.UserID, FirstName, LastName, UserName, 
		EmailAddress, PhoneNumber, IsVerified
	FROM dbo.ApplicationUser au with(nolock)
		JOIN dbo.Role r ON au.RoleID = r.RoleID
		WHERE au.UserID = @UserID;

	SELECT a.* 
	FROM Address a
		JOIN UserAddress ua ON ua.AddressID = a.AddressID
		WHERE ua.UserID = @UserID;
END
GO
