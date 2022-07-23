USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	 Jason Howie
-- Create date: 7/23/2022
-- Description:	Sets reshres token to isused
-- =============================================
DROP PROCEDURE IF EXISTS dbo.SetRefreshTokenToIsUsed
GO
CREATE PROCEDURE dbo.SetRefreshTokenToIsUsed
	@TokenID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE RefreshToken
	SET IsUsed = 1
	WHERE TokenID = @TokenID

	RETURN @@ROWCOUNT
END
GO
