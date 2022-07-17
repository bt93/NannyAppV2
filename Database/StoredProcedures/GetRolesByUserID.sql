USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/9/2022
-- Description:	Gets the users roles
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetRolesByUserID
GO
CREATE PROCEDURE dbo.GetRolesByUserID
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT r.RoleID AS Role FROM Role r WITH(NOLOCK)
		JOIN UserRole ur ON ur.RoleID = r.RoleID
		WHERE ur.UserID = @UserID;
END
GO
