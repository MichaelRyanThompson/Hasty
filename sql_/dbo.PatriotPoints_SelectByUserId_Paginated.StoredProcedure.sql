USE [Hasty]
GO
/****** Object:  StoredProcedure [dbo].[PatriotPoints_SelectByUserId_Paginated]    Script Date: 4/13/2023 5:40:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Michael Ryan Thompson
-- Create date: 3/13/2023
-- Description: PatriotPoints Select By UserId Paginated proc
-- Code Reviewer: Andres Correa

-- MODIFIED BY: Michael Ryan Thompson
-- MODIFIED DATE: 3/27/2023
-- Code Reviewer: Eugene Kim
-- Note: Added Description and ImageUrl
-- =============================================
CREATE PROC [dbo].[PatriotPoints_SelectByUserId_Paginated]
	
			 @PageIndex INT
			,@PageSize INT
			,@UserId INT

as
/*----------Test Code-------------------

	DECLARE  @PageIndex int= 0
			,@PageSize int = 20
			,@UserId int = 73
	
	EXECUTE [dbo].[PatriotPoints_SelectByUserId_Paginated] 
											  @PageIndex
											 ,@PageSize
											 ,@UserId
														  

	SELECT *
	From dbo.PatriotPoints

	 
	
	
*/

BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT   pp.[Id]
			,pp.[SourceId]
			,pp.[Points]
			,pp.[DateCreated]
			,ps.[Id]
			,ps.[Name]
			,ps.[PointsAwarded]
			,ps.[Description]
			,ps.[ImageUrl]
			,ps.[IsExpired]
			,ps.[IsDeleted]
			,ps.[DateCreated] 
			,ps.[DateModified]
			,ps.[CreatedBy]
			,ps.[ModifiedBy]
			,TotalCount = COUNT(1) OVER()
			

	 FROM [dbo].[PatriotPoints] as pp
	 INNER JOIN [dbo].[PatriotPointsSource] as ps
	 ON pp.[SourceId] = ps.[Id]
	WHERE pp.[UserId] = @UserId
	ORDER BY pp.[Id]

	 OFFSET @offSet ROWS
	 FETCH NEXT @PageSize ROWS ONLY

END
GO
