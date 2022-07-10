USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/10/2022
-- Description:	Adds a new address
-- =============================================
DROP PROCEDURE IF EXISTS dbo.AddNewAddress
GO
CREATE PROCEDURE dbo.AddNewAddress
	@Address1 VARCHAR(200),
	@Address2 VARCHAR(200) = '',
	@Address3 VARCHAR(200) = '',
	@Address4 VARCHAR(200) = '',
	@Locality VARCHAR(200),
	@Region VARCHAR(200),
	@PostalCode VARCHAR(10),
	@County VARCHAR(60),
	@Country VARCHAR(60),
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	INSERT INTO Address (Address1, Address2, Address3, Address4, Locality, Region, PostalCode, County, Country)
		VALUES (@Address1, @Address2, @Address3, @Address4, @Locality, @Region, @PostalCode, @County, @Country)
	
	DECLARE @AddressID INT
	SET @AddressID = @@IDENTITY

	INSERT INTO UserAddress (AddressID, UserID)
		VALUES (@AddressID, @UserID)
	
	COMMIT TRANSACTION
	RETURN @AddressID;
END
GO
