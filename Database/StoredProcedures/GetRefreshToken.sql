USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/23/2022
-- Description: Gets a refresh token
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetRefreshToken
GO
CREATE PROCEDURE dbo.GetRefreshToken 
	@Token VARCHAR(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM RefreshToken WITH(NOLOCK)
		WHERE Token = @Token
END
GO
