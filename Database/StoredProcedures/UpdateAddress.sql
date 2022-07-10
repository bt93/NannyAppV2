USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/10/2022
-- Description:	Updates an address
-- =============================================
DROP PROCEDURE IF EXISTS dbo.UpdateAddress
GO
CREATE PROCEDURE dbo.UpdateAddress 
	@UserID INT,
	@AddressID INT,
	@Address1 VARCHAR(200),
	@Address2 VARCHAR(200),
	@Address3 VARCHAR(200),
	@Address4 VARCHAR(200),
	@Locality VARCHAR(200),
	@Region VARCHAR(200),
	@PostalCode VARCHAR(10),
	@County VARCHAR(60),
	@Country VARCHAR(60)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS (SELECT UserID FROM UserAddress
				WHERE UserID = @UserID
				AND AddressID = @AddressID)
				
				UPDATE Address
				SET Address1 = @Address1,
				Address2 = @Address2,
				@Address3 = @Address3,
				@Address4 = @Address4,
				Locality = @Locality,
				Region = @Region,
				PostalCode = @PostalCode,
				County = @County,
				Country = @Country
				WHERE AddressID = @AddressID
				RETURN @@RowCount
END
GO
