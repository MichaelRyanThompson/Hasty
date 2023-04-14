USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPointsSource_Update_IsDeleted]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPointsSource Update IsDeleted proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE PROC [dbo].[PatriotPointsSource_Update_IsDeleted]

	 @Id INT 
	,@ModifiedBy INT
	

/*
----TEST CODE----
DECLARE @Id INT = 3
		,@ModifiedBy INT = 5

SELECT *
FROM dbo.PatriotPointsSource
Where Id = @Id

EXECUTE [dbo].[PatriotPointsSource_Update_IsDeleted] 
		 @Id
		,@ModifiedBy
		
SELECT *
FROM dbo.PatriotPointsSource
Where Id = @Id


*/

AS

BEGIN

	DECLARE @datNow datetime2 = getutcdate()

	UPDATE [dbo].[PatriotPointsSource]
		
		SET [IsDeleted] = 1
			,[ModifiedBy] = @ModifiedBy
			,[DateModified] = @datNow
			
	WHERE Id = @Id

END
GO
