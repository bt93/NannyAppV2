USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jason Howie
-- Create date: 7/3/2022
-- Description:	Retrieves a list of children by the associated users id
-- =============================================
DROP PROCEDURE IF EXISTS dbo.GetChildrenByUserID
GO

CREATE PROCEDURE dbo.GetChildrenByUserID 
	@UserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM Child c WITH(NOLOCK)
		JOIN ChildUser cu ON c.ChildID = cu.ChildID
		WHERE cu.UserID = @UserID;
END
GO
