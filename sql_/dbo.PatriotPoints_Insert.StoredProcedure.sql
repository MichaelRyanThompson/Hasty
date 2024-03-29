USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPoints_Insert]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPoints Insert proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY:
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[PatriotPoints_Insert]
		
		 @UserId int
		,@SourceId int
		,@Id int OUTPUT
		

/* --------  Test Code  ----------- 


	Declare @Id int = 0;


	Declare  @UserId int = 14
			,@SourceId int = 7
			
			

Execute dbo.PatriotPoints_Insert 
							 @UserId
							,@SourceId
							,@Id OUTPUT

Select *
	From dbo.PatriotPoints
	Where Id = @Id

*/

as
 
BEGIN

DECLARE @PointsAwarded int = 0;

SELECT  @PointsAwarded = PointsAwarded 
FROM dbo.PatriotPointsSource as ps
where ps.Id = @SourceId

Insert into [dbo].[PatriotPoints]
				 ([UserId]
				 ,[SourceId]
				 ,[Points])

	Values
			(@UserId
			,@SourceId
			,@PointsAwarded)
	
	SET @Id = SCOPE_IDENTITY()
	

END
GO
