USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/20/2022
-- Description:	Adds new User
-- =============================================
DROP PROCEDURE IF EXISTS dbo.AddNewUser
GO
CREATE PROCEDURE dbo.AddNewUser
	@FirstName VARCHAR(80),
	@LastName VARCHAR(80),
	@UserName VARCHAR(80),
	@EmailAddress VARCHAR(120),
	@Password NVARCHAR(250),
	@PhoneNumber VARCHAR(20),
	@Salt NVARCHAR(20),
	@Role VARCHAR(10),
	@Address1 VARCHAR(200),
	@Address2 VARCHAR(200) = '',
	@Address3 VARCHAR(200) = '',
	@Address4 VARCHAR(200) = '',
	@Locality VARCHAR(200),
	@Region VARCHAR(200),
	@PostalCode VARCHAR(10),
	@County VARCHAR(60),
	@Country VARCHAR(60),
	@UserID INT = 0 OUT,
	@AddressID INT = 0 OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

    INSERT INTO ApplicationUser (FirstName, LastName, UserName, EmailAddress, Password, PhoneNumber, Salt, RoleID)
		VALUES (@FirstName, @LastName, @UserName, @EmailAddress, @Password, @PhoneNumber, @Salt, (SELECT RoleID FROM Role WHERE RoleName = @Role));
	SET @UserID = @@IDENTITY;

	INSERT INTO Address (Address1, Address2, Address3, Address4, Locality, Region, PostalCode, County, Country)
		VALUES (@Address1, @Address2, @Address3, @Address4, @Locality, @Region, @PostalCode, @County, @Country);
	SET @AddressID = @@IDENTITY;

	INSERT INTO UserAddress (UserID, AddressID)
		VALUES (@UserID, @AddressID);

	SELECT @UserID AS UserID

	COMMIT TRANSACTION
END
GO
