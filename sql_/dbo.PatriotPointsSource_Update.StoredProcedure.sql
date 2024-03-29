USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPointsSource_Update]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPointsSource Update proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY: Michael Thompson
-- MODIFIED DATE: 3/27/2023
-- Code Reviewer: Eugene Kim
-- Note: Added Description and ImageUrl
-- =============================================
CREATE PROC [dbo].[PatriotPointsSource_Update]
	 @Name NVARCHAR(50)
	,@PointsAwarded INT
	,@Description NVARCHAR(255)
	,@ImageUrl NVARCHAR(255)
	,@ModifiedBy INT
	,@Id INT 

/*
----TEST CODE----
DECLARE @Id int = 1;

DECLARE  @Name NVARCHAR(50) = 'DELETE ME UPDATE'
		,@PointsAwarded INT = 50
		,@Description NVARCHAR(255) = 'DELETE ME PLEASE'
		,@ImageUrl NVARCHAR(255) = 'deleteme.jpg'
		,@ModifiedBy INT = 13

SELECT *
FROM dbo.PatriotPointsSource
Where Id = @Id

EXECUTE [dbo].[PatriotPointsSource_Update]
		 @Name
		,@PointsAwarded
		,@Description
		,@ImageUrl
		,@ModifiedBy
		,@Id 


SELECT *
FROM dbo.PatriotPointsSource
Where Id = @Id


*/

AS

BEGIN

	DECLARE @DateNow DATETIME2(7) = GETUTCDATE()

	UPDATE [dbo].[PatriotPointsSource]
		SET  [Name] = @Name
			,[PointsAwarded]= @PointsAwarded
			,[Description] = @Description
			,[ImageUrl] = @ImageUrl
			,[ModifiedBy] = @ModifiedBy
			,[DateModified] = @DateNow
	WHERE Id = @Id

END
GO
