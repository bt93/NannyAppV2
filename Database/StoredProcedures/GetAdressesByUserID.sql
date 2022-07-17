Use NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 6/19/2022
-- Description:	Gets a list of addresses by the user id
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetAdressesByUserID
GO
CREATE PROCEDURE dbo.GetAdressesByUserID
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT a.*, ua.UserID
	FROM Address a WITH(NOLOCK)
	JOIN UserAddress ua ON ua.AddressID = a.AddressID
	WHERE ua.UserID = @UserID;

END
GO
