USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPointsSource_Insert]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPointsSource Insert proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY: Michael Thompson
-- MODIFIED DATE: 3/27/2023
-- Code Reviewer: Eugene Kim
-- Note: Added Description and ImageUrl
-- =============================================
CREATE proc [dbo].[PatriotPointsSource_Insert]
		
		 @Name NVARCHAR(50)
		,@PointsAwarded INT
		,@Description NVARCHAR(255)
		,@ImageUrl NVARCHAR(255)
		,@UserId INT
		,@Id INT OUTPUT
		

/* --------  Test Code  ----------- 


	Declare @Id INT = 0;

	Declare  @Name NVARCHAR(50) = 'Test Insert DELETE ME'
			,@PointsAwarded INT = 100
			,@Description NVARCHAR(255) = 'Test Insert Description'
			,@ImageUrl NVARCHAR(255) = 'Testimage.jpg'
			,@UserId INT = 14
			

Execute dbo.PatriotPointsSource_Insert 
							 @Name
							,@PointsAwarded
							,@Description
							,@ImageUrl
							,@UserId
							,@Id OUTPUT

Select *
	From dbo.PatriotPointsSource
	Where Id = @Id

*/

as
 
BEGIN

Insert into [dbo].[PatriotPointsSource]
				 ([Name]
				 ,[PointsAwarded]
				 ,[Description]
				 ,[ImageUrl]
				 ,[CreatedBy]
				 ,[ModifiedBy])

	Values
			(@Name
			,@PointsAwarded
			,@Description
			,@ImageUrl
			,@UserId
			,@UserId)
	
	SET @Id = SCOPE_IDENTITY()

END
GO
