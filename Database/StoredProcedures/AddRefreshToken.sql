USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/23/2022
-- Description: Adds new Refresh token
-- =============================================
DROP PROCEDURE IF EXISTS dbo.AddRefreshToken
GO
CREATE PROCEDURE dbo.AddRefreshToken
	@UserID INT,
	@Token VARCHAR(250),
	@JWTID VARCHAR(250),
	@IsUsed BIT,
	@IsRevoked BIT,
	@DateAdded DATETIMEOFFSET(7),
	@DateExpired DATETIMEOFFSET(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO RefreshToken (UserID, Token, JWTID, IsUsed, IsRevoked, DateAdded, DateExpired)
		VALUES (@UserID, @Token, @JWTID, @IsUsed, @IsRevoked, @DateAdded, @DateExpired)

	Return @@RowCount
END
GO
